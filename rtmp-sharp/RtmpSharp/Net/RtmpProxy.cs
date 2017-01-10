// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Net.RtmpProxy
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using ezBot;
using RtmpSharp.IO;
using RtmpSharp.Messaging;
using RtmpSharp.Messaging.Messages;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace RtmpSharp.Net
{
    public class RtmpProxy
    {
        private readonly RemoteCertificateValidationCallback _certificateValidator = (RemoteCertificateValidationCallback)((sender, certificate, chain, errors) => true);
        private readonly X509Certificate2 _cert;
        private readonly TcpListener _listener;
        private readonly Uri _remoteUri;
        private readonly SerializationContext _serializationContext;
        private RtmpProxyRemote _remote;
        private RtmpProxySource _source;
        private IPEndPoint _sourceEndpoint;

        public event EventHandler<RemotingMessageReceivedEventArgs> RemotingMessageReceived;

        public event EventHandler<RemotingMessageReceivedEventArgs> ErrorMessageReceived;

        public event EventHandler<RemotingMessageReceivedEventArgs> AcknowledgeMessageReceived;

        public event EventHandler<MessageReceivedEventArgs> AsyncMessageReceived;

        public event EventHandler<EventArgs> Connected;

        public event EventHandler<EventArgs> Disconnected;

        public RtmpProxy(IPEndPoint source, Uri remote, SerializationContext context, X509Certificate2 cert = null)
        {
            this._cert = cert;
            this._serializationContext = context;
            this._remoteUri = remote;
            this._sourceEndpoint = source;
            this._listener = new TcpListener(source);
        }

        public void Listen()
        {
            this._listener.Start();
            this._listener.BeginAcceptTcpClient(new AsyncCallback(this.OnClientAccepted), (object)this._listener);
        }

        public void Close()
        {
            this._listener.Stop();
        }

        private void OnClientAccepted(IAsyncResult ar)
        {
            TcpListener asyncState = ar.AsyncState as TcpListener;
            try
            {
                TcpClient client = asyncState.EndAcceptTcpClient(ar);
                if (!client.Connected)
                    return;
                this._source = new RtmpProxySource(this._serializationContext, this.GetRtmpStream(client));
                this._source.RemotingMessageReceived += new EventHandler<RemotingMessageReceivedEventArgs>(this.OnRemotingMessageReceived);
                this._source.CommandMessageReceived += new EventHandler<CommandMessageReceivedEventArgs>(this.OnCommandMessageReceived);
                this._source.ConnectMessageReceived += new EventHandler<ConnectMessageEventArgs>(this.OnConnectMessageReceived);
                this._source.Disconnected += new EventHandler(this.OnClientDisconnected);
            }
            catch (ObjectDisposedException ex)
            {
                Tools.Log(ex.StackTrace);
            }
        }

        private void OnServerDisconnected(object sender, EventArgs e)
        {
            this._remote.Close();
            this._listener.Stop();
            // ISSUE: reference to a compiler-generated field
            EventHandler<EventArgs> disconnected = this.Disconnected;
            if (disconnected != null)
            {
                EventArgs e1 = new EventArgs();
                disconnected((object)this, e1);
            }
            this.Listen();
        }

        private void OnClientDisconnected(object sender, EventArgs e)
        {
            this._source.Close();
            this._listener.Stop();
            // ISSUE: reference to a compiler-generated field
            EventHandler<EventArgs> disconnected = this.Disconnected;
            if (disconnected != null)
            {
                EventArgs e1 = new EventArgs();
                disconnected((object)this, e1);
            }
            this.Listen();
        }

        private void OnConnectMessageReceived(object sender, ConnectMessageEventArgs e)
        {
            if (e.Message.Operation == CommandOperation.ClientPing)
            {
                this._remote = new RtmpProxyRemote(this._remoteUri, this._serializationContext, ObjectEncoding.Amf3);
                this._remote.MessageReceived += new EventHandler<MessageReceivedEventArgs>(this.OnAsyncMessageReceived);
                this._remote.Disconnected += new EventHandler(this.OnServerDisconnected);
                e.Result = this._remote.ConnectAckAsync(e.InvokeId, e.ConnectionParameters, (object)false, (object)e.ClientId, (object)e.AuthToken, (object)e.Message).Result;
                // ISSUE: reference to a compiler-generated field
                EventHandler<EventArgs> connected = this.Connected;
                if (connected == null)
                    return;
                EventArgs e1 = new EventArgs();
                connected((object)this, e1);
            }
            else
            {
                this._remote = new RtmpProxyRemote(this._remoteUri, this._serializationContext, ObjectEncoding.Amf3);
                this._remote.MessageReceived += new EventHandler<MessageReceivedEventArgs>(this.OnAsyncMessageReceived);
                this._remote.Disconnected += new EventHandler(this.OnServerDisconnected);
                e.Result = this._remote.ReconnectAckAsync(e.InvokeId, e.ConnectionParameters, (object)false, (object)e.ClientId, (object)e.AuthToken, (object)e.Message).Result;
                // ISSUE: reference to a compiler-generated field
                EventHandler<EventArgs> connected = this.Connected;
                if (connected == null)
                    return;
                EventArgs e1 = new EventArgs();
                connected((object)this, e1);
            }
        }

        private void OnAsyncMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler<MessageReceivedEventArgs> asyncMessageReceived = this.AsyncMessageReceived;
            if (asyncMessageReceived != null)
            {
                MessageReceivedEventArgs e1 = e;
                asyncMessageReceived((object)this, e1);
            }
            this._source.InvokeReceive(e.ClientId, e.Subtopic, e.Message.Body);
        }

        private void OnCommandMessageReceived(object sender, CommandMessageReceivedEventArgs e)
        {
            e.Result = this._remote.InvokeAckAsync(e.InvokeId, (string)null, e.Message).Result;
        }

        private void OnRemotingMessageReceived(object sender, RemotingMessageReceivedEventArgs e)
        {
            try
            {
                // ISSUE: reference to a compiler-generated field
                EventHandler<RemotingMessageReceivedEventArgs> remotingMessageReceived = this.RemotingMessageReceived;
                if (remotingMessageReceived != null)
                {
                    RemotingMessageReceivedEventArgs e1 = e;
                    remotingMessageReceived((object)this, e1);
                }
                e.Result = this._remote.InvokeAckAsync(e.InvokeId, e.Message).Result;
                // ISSUE: reference to a compiler-generated field
                EventHandler<RemotingMessageReceivedEventArgs> acknowledgeMessageReceived = this.AcknowledgeMessageReceived;
                if (acknowledgeMessageReceived == null)
                    return;
                RemotingMessageReceivedEventArgs e2 = e;
                acknowledgeMessageReceived((object)this, e2);
            }
            catch (AggregateException ex)
            {
                InvocationException innerException = ex.InnerException as InvocationException;
                if (innerException != null)
                {
                    e.Error = (ErrorMessage)innerException.SourceException;
                    // ISSUE: reference to a compiler-generated field
                    EventHandler<RemotingMessageReceivedEventArgs> errorMessageReceived = this.ErrorMessageReceived;
                    if (errorMessageReceived == null)
                        return;
                    RemotingMessageReceivedEventArgs e1 = e;
                    errorMessageReceived((object)this, e1);
                }
                else
                    throw;
            }
        }

        public async Task<object> InvokeAsync(string destination, string operation, params object[] arguments)
        {
            return await this._remote.InvokeAsync<object>("my-rtmps", destination, operation, arguments);
        }

        private Stream GetRtmpStream(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            if (this._cert == null)
                return (Stream)stream;
            SslStream sslStream = new SslStream((Stream)stream, false, this._certificateValidator);
            X509Certificate2 cert = this._cert;
            sslStream.AuthenticateAsServer((X509Certificate)cert);
            return (Stream)sslStream;
        }
    }
}