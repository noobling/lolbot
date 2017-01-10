using Complete;
using Complete.Threading;
using ezBot;
using RtmpSharp.IO;
using RtmpSharp.Messaging;
using RtmpSharp.Messaging.Events;
using RtmpSharp.Messaging.Messages;
using System;
using System.IO;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;

namespace RtmpSharp.Net
{
    internal class RtmpProxySource
    {
        private readonly RemoteCertificateValidationCallback _certificateValidator = (sender, certificate, chain, errors) => true;
        public bool NoDelay = true;
        private readonly TaskCallbackManager<int, object> _callbackManager;
        private readonly ObjectEncoding _objectEncoding;
        private readonly SerializationContext _serializationContext;
        private int _invokeId;
        private RtmpPacketReader _reader;
        private Thread _readerThread;
        private RtmpPacketWriter _writer;
        private Thread _writerThread;

        public bool IsDisconnected { get; set; }

        public event EventHandler Disconnected;

        internal event EventHandler<RemotingMessageReceivedEventArgs> RemotingMessageReceived;

        internal event EventHandler<CommandMessageReceivedEventArgs> CommandMessageReceived;

        internal event EventHandler<ConnectMessageEventArgs> ConnectMessageReceived;

        public event EventHandler<Exception> CallbackException;

        public RtmpProxySource(SerializationContext serializationContext, Stream stream) : this(serializationContext)
        {
            DoHandshake(stream);
            EstablishThreads(stream);
            _objectEncoding = ObjectEncoding.Amf3;
        }

        public RtmpProxySource(SerializationContext serializationContext)
        {
            if (serializationContext == null)
                throw new ArgumentNullException("serializationContext");
            _serializationContext = serializationContext;
            _callbackManager = new TaskCallbackManager<int, object>();
        }

        private void DoHandshake(Stream stream)
        {
            RtmpHandshake.Read(stream, true);
            Random random = new Random();
            byte[] numArray = new byte[1528];
            byte[] buffer = numArray;
            random.NextBytes(buffer);
            RtmpHandshake h = new RtmpHandshake() { Version = 3, Time = (uint)Environment.TickCount, Time2 = 0, Random = numArray };
            RtmpHandshake h2 = h.Clone();
            h2.Time2 = (uint)Environment.TickCount;
            RtmpHandshake.WriteAsync(stream, h, h2, true);
            RtmpHandshake.Read(stream, false);
        }

        private Task<object> QueueCommandAsTask(Command command, int streamId, int messageStreamId)
        {
            if (IsDisconnected)
                return CreateExceptedTask(new ClientDisconnectedException("disconnected"));
            Task<object> task = this._callbackManager.Create(command.InvokeId);
            _writer.Queue(command, streamId, messageStreamId);
            return task;
        }

        public void EstablishThreads(Stream stream)
        {
            _writer = new RtmpPacketWriter(new AmfWriter(stream, _serializationContext), ObjectEncoding.Amf3);
            _reader = new RtmpPacketReader(new AmfReader(stream, _serializationContext));
            _reader.EventReceived += new EventHandler<EventReceivedEventArgs>(EventReceivedCallback);
            _reader.Disconnected += new EventHandler<ExceptionalEventArgs>(OnPacketProcessorDisconnected);
            _writer.Disconnected += new EventHandler<ExceptionalEventArgs>(OnPacketProcessorDisconnected);
            _writerThread = new Thread(new ThreadStart(_reader.ReadLoop))
            {
                IsBackground = true
            };
            _readerThread = new Thread(new ThreadStart(_writer.WriteLoop))
            {
                IsBackground = true
            };
            _writerThread.Start();
            _readerThread.Start();
        }

        private void OnPacketProcessorDisconnected(object sender, ExceptionalEventArgs e)
        {
            OnDisconnect(e);
        }

        private void OnDisconnect(ExceptionalEventArgs e)
        {
            if (IsDisconnected)
                return;
            this.IsDisconnected = true;
            if (_writer != null)
                _writer.Continue = false;
            if (_reader != null)
                _reader.Continue = false;
            WrapCallback(() => this._callbackManager.SetExceptionForAll(new ClientDisconnectedException(e.Description, e.Exception)));
            _invokeId = 0;
            WrapCallback(() =>
           {
               // ISSUE: reference to a compiler-generated field
               EventHandler disconnected = Disconnected;
               if (disconnected == null)
                   return;
               ExceptionalEventArgs exceptionalEventArgs = e;
               disconnected(this, exceptionalEventArgs);
           });
        }

        public void Close()
        {
            this.OnDisconnect(new ExceptionalEventArgs("closed"));
        }

        private async void EventReceivedCallback(object sender, EventReceivedEventArgs e)
        {
            try
            {
                Command command;
                object param;
                switch (e.Event.MessageType)
                {
                    case MessageType.UserControlMessage:
                    UserControlMessage userControlMessage = (UserControlMessage)e.Event;
                    if (userControlMessage.EventType == UserControlMessageType.PingRequest)
                    {
                        WriteProtocolControlMessage(new UserControlMessage(UserControlMessageType.PingResponse, userControlMessage.Values));
                        break;
                    }
                    break;

                    case MessageType.CommandAmf3:
                    case MessageType.DataAmf0:
                    case MessageType.CommandAmf0:
                    command = (Command)e.Event;
                    Method methodCall = command.MethodCall;
                    param = methodCall.Parameters.Length == 1 ? methodCall.Parameters[0] : (object)methodCall.Parameters;
                    if (methodCall.Name == "_result" || methodCall.Name == "_error" || methodCall.Name == "receive")
                        throw new InvalidDataException();
                    if (!(methodCall.Name == "onstatus"))
                    {
                        if (methodCall.Name == "connect")
                        {
                            CommandMessage parameter = (CommandMessage)methodCall.Parameters[3];
                            object obj1;
                            parameter.Headers.TryGetValue("DSEndpoint", out obj1);
                            object obj2;
                            parameter.Headers.TryGetValue("DSId", out obj2);
                            ConnectMessageEventArgs args = new ConnectMessageEventArgs((string)methodCall.Parameters[1], (string)methodCall.Parameters[2], parameter, (string)obj1, (string)obj2, command.InvokeId, (AsObject)command.ConnectionParameters);
                            
                            ConnectMessageReceived?.Invoke(this, args);
                            if (parameter.Operation == CommandOperation.ClientPing)
                            {
                                AsObject asObject1 = await InvokeConnectResultAsync(command.InvokeId, (AsObject)args.Result.Body);
                            }
                            else
                            {
                                AsObject asObject2 = await InvokeReconnectResultInvokeAsync(command.InvokeId, (AsObject)args.Result.Body);
                            }
                            args = null;
                            break;
                        }
                        if (param is RemotingMessage)
                        {
                            RemotingMessage message = param as RemotingMessage;
                            object obj1;
                            message.Headers.TryGetValue("DSEndpoint", out obj1);
                            object obj2;
                            message.Headers.TryGetValue("DSId", out obj2);
                            string endpoint = (string)obj1;
                            string clientId = (string)obj2;
                            int invokeId = command.InvokeId;
                            RemotingMessageReceivedEventArgs receivedEventArgs = new RemotingMessageReceivedEventArgs(message, endpoint, clientId, invokeId);
                            
                            RemotingMessageReceived?.Invoke(this, receivedEventArgs);
                            if (receivedEventArgs.Error == null)
                            {
                                InvokeResult(command.InvokeId, receivedEventArgs.Result);
                                break;
                            }
                            InvokeError(command.InvokeId, receivedEventArgs.Error);
                            break;
                        }
                        if (param is CommandMessage)
                        {
                            CommandMessage message = param as CommandMessage;
                            object obj1;
                            message.Headers.TryGetValue("DSEndpoint", out obj1);
                            object obj2;
                            message.Headers.TryGetValue("DSId", out obj2);
                            string endpoint = obj1 as string;
                            string dsId = obj2 as string;
                            int invokeId = command.InvokeId;
                            CommandMessageReceivedEventArgs receivedEventArgs = new CommandMessageReceivedEventArgs(message, endpoint, dsId, invokeId);
                            
                            CommandMessageReceived?.Invoke(this, receivedEventArgs);
                            InvokeResult(command.InvokeId, receivedEventArgs.Result);
                            break;
                        }
                        break;
                    }
                    break;
                }
                command = null;
                param = null;
            }
            catch (ClientDisconnectedException ex)
            {
                Tools.Log(ex.StackTrace);
            }
        }

        internal void InvokeResult(int invokeId, AcknowledgeMessageExt message)
        {
            if (_objectEncoding != ObjectEncoding.Amf3)
                throw new NotSupportedException("Flex RPC requires AMF3 encoding.");
            InvokeAmf3 invokeAmf3 = new InvokeAmf3();
            invokeAmf3.InvokeId = invokeId;
            invokeAmf3.MethodCall = new Method("_result", new object[1]
            {
                message
            }, 1 != 0, CallStatus.Result);
            QueueCommandAsTask(invokeAmf3, 3, 0);
        }

        internal void InvokeError(int invokeId, ErrorMessage message)
        {
            if (_objectEncoding != ObjectEncoding.Amf3)
                throw new NotSupportedException("Flex RPC requires AMF3 encoding.");
            InvokeAmf3 invokeAmf3 = new InvokeAmf3();
            invokeAmf3.InvokeId = invokeId;
            invokeAmf3.MethodCall = new Method("_error", new object[1]
            {
                message
            }, 0 != 0, CallStatus.Result);
            QueueCommandAsTask(invokeAmf3, 3, 0);
        }

        public void InvokeError(int invokeId, string correlationId, object rootCause, string faultDetail, string faultString, string faultCode)
        {
            InvokeError(invokeId, new ErrorMessage()
            {

                ClientId = Uuid.NewUuid(),
                MessageId = Uuid.NewUuid(),
                CorrelationId = correlationId,
                RootCause = rootCause
            });
        }

        internal void InvokeReceive(string clientId, string subtopic, object body)
        {
            InvokeAmf3 invokeAmf3_1 = new InvokeAmf3();
            invokeAmf3_1.InvokeId = 0;
            InvokeAmf3 invokeAmf3_2 = invokeAmf3_1;
            string methodName = "receive";
            object[] parameters = new object[1];
            int index = 0;
            AsyncMessageExt asyncMessageExt = new AsyncMessageExt();
            asyncMessageExt.Headers = new AsObject()
            {
                {
                    "DSSubtopic",
                    subtopic
                }
            };
            string str1 = clientId;
            asyncMessageExt.ClientId = str1;
            object obj = body;
            asyncMessageExt.Body = obj;
            string str2 = Uuid.NewUuid();
            asyncMessageExt.MessageId = str2;
            parameters[index] = asyncMessageExt;
            int num1 = 1;
            int num2 = 0;
            Method method = new Method(methodName, parameters, num1 != 0, (CallStatus)num2);
            invokeAmf3_2.MethodCall = method;
            this.QueueCommandAsTask(invokeAmf3_1, 3, 0);
        }

        public async Task<AsObject> ConnectResultInvokeAsync(object[] parameters)
        {
            this.WriteProtocolControlMessage((RtmpEvent)new WindowAcknowledgementSize(245248000));
            this.WriteProtocolControlMessage((RtmpEvent)new PeerBandwidth(250000, (byte)2));
            this.SetChunkSize(50000);
            InvokeAmf0 invokeAmf0 = new InvokeAmf0();
            invokeAmf0.MethodCall = new Method("_result", new object[1]
            {
                new AsObject()
                {
                    {
                    "objectEncoding",
                     3.0
                    },
                    {
                    "level",
                     "status"
                    },
                    {
                    "details",
                     null
                    },
                    {
                    "description",
                     "Connection succeeded."
                    },
                    {
                    "DSMessagingVersion",
                     1.0
                    },
                    {
                    "code",
                     "NetConnection.Connect.Success"
                    },
                    {
                    "id",
                     Uuid.NewUuid()
                    }
                }
            }, 1 != 0, CallStatus.Request);
            invokeAmf0.InvokeId = GetNextInvokeId();
            return (AsObject)await QueueCommandAsTask(invokeAmf0, 3, 0);
        }

        public async Task<AsObject> InvokeConnectResultAsync(int invokeId, AsObject param)
        {
            this.WriteProtocolControlMessage((RtmpEvent)new WindowAcknowledgementSize(245248000));
            this.WriteProtocolControlMessage((RtmpEvent)new PeerBandwidth(250000, (byte)2));
            this.SetChunkSize(50000);
            InvokeAmf0 invokeAmf0 = new InvokeAmf0();
            invokeAmf0.MethodCall = new Method("_result", new object[1]
            {
        (object) param
            }, 1 != 0, CallStatus.Request);
            invokeAmf0.InvokeId = invokeId;
            return (AsObject)await this.QueueCommandAsTask((Command)invokeAmf0, 3, 0);
        }

        public async Task<AsObject> InvokeReconnectResultInvokeAsync(int invokeId, AsObject param)
        {
            this.SetChunkSize(50000);
            InvokeAmf0 invokeAmf0 = new InvokeAmf0();
            invokeAmf0.MethodCall = new Method("_result", new object[1]
            {
        (object) param
            }, 1 != 0, CallStatus.Request);
            invokeAmf0.InvokeId = invokeId;
            return (AsObject)await this.QueueCommandAsTask((Command)invokeAmf0, 3, 0);
        }

        public void SetChunkSize(int size)
        {
            WriteProtocolControlMessage(new ChunkSize(size));
        }

        private void WriteProtocolControlMessage(RtmpEvent @event)
        {
            _writer.Queue(@event, 2, 0);
        }

        private int GetNextInvokeId()
        {
            return Interlocked.Increment(ref _invokeId);
        }

        private void WrapCallback(Action action)
        {
            try
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    CallbackException?.Invoke(this, ex);
                }
            }
            catch (Exception)
            {
            }
        }

        private static Task<object> CreateExceptedTask(Exception exception)
        {
            TaskCompletionSource<object> completionSource = new TaskCompletionSource<object>();
            completionSource.SetException(exception);
            return completionSource.Task;
        }
    }
}