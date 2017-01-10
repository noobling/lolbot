// Decompiled with JetBrains decompiler
// Type: Complete.Threading.TaskCallbackManager`2
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Complete.Threading
{
  internal class TaskCallbackManager<K, V>
  {
    private readonly ConcurrentDictionary<K, TaskCompletionSource<V>> _callbacks;

    public TaskCallbackManager()
    {
      this._callbacks = new ConcurrentDictionary<K, TaskCompletionSource<V>>();
    }

    public Task<V> Create(K key)
    {
      return this._callbacks.GetOrAdd(key, (Func<K, TaskCompletionSource<V>>) (k => new TaskCompletionSource<V>())).Task;
    }

    public bool Remove(K key)
    {
      TaskCompletionSource<V> completionSource;
      return this._callbacks.TryRemove(key, out completionSource);
    }

    public void SetResult(K key, V result)
    {
      TaskCompletionSource<V> completionSource;
      if (!this._callbacks.TryRemove(key, out completionSource))
        return;
      completionSource.TrySetResult(result);
    }

    public void SetException(K key, Exception exception)
    {
      TaskCompletionSource<V> completionSource;
      if (!this._callbacks.TryRemove(key, out completionSource))
        return;
      completionSource.TrySetException(exception);
    }

    public void SetResultForAll(V result)
    {
      TaskCompletionSource<V>[] array = this._callbacks.Select<KeyValuePair<K, TaskCompletionSource<V>>, TaskCompletionSource<V>>((Func<KeyValuePair<K, TaskCompletionSource<V>>, TaskCompletionSource<V>>) (x => x.Value)).ToArray<TaskCompletionSource<V>>();
      this._callbacks.Clear();
      foreach (TaskCompletionSource<V> completionSource in array)
        completionSource.TrySetResult(result);
    }

    public void SetExceptionForAll(Exception exception)
    {
      TaskCompletionSource<V>[] array = this._callbacks.Select<KeyValuePair<K, TaskCompletionSource<V>>, TaskCompletionSource<V>>((Func<KeyValuePair<K, TaskCompletionSource<V>>, TaskCompletionSource<V>>) (x => x.Value)).ToArray<TaskCompletionSource<V>>();
      this._callbacks.Clear();
      foreach (TaskCompletionSource<V> completionSource in array)
        completionSource.TrySetException(exception);
    }

    public void Clear()
    {
      this._callbacks.Clear();
    }
  }
}
