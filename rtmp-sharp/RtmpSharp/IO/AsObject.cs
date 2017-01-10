// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AsObject
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace RtmpSharp.IO
{
  [TypeConverter(typeof (AsObjectConverter))]
  [Serializable]
  public class AsObject : DynamicObject, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
  {
    private readonly Dictionary<string, object> _underlying;

    public string TypeName { get; set; }

    public bool IsTyped
    {
      get
      {
        return !string.IsNullOrEmpty(this.TypeName);
      }
    }

    public int Count
    {
      get
      {
        return this._underlying.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return ((ICollection<KeyValuePair<string, object>>) this._underlying).IsReadOnly;
      }
    }

    public ICollection<string> Keys
    {
      get
      {
        return (ICollection<string>) this._underlying.Keys;
      }
    }

    public ICollection<object> Values
    {
      get
      {
        return (ICollection<object>) this._underlying.Values;
      }
    }

    public object this[string key]
    {
      get
      {
        return this._underlying[key];
      }
      set
      {
        this._underlying[key] = value;
      }
    }

    public AsObject()
    {
      this._underlying = new Dictionary<string, object>();
    }

    public AsObject(string typeName)
      : this()
    {
      this.TypeName = typeName;
    }

    public AsObject(Dictionary<string, object> dictionary)
      : this()
    {
      this._underlying = new Dictionary<string, object>((IDictionary<string, object>) dictionary);
    }

    public override IEnumerable<string> GetDynamicMemberNames()
    {
      return (IEnumerable<string>) this._underlying.Keys;
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
      return this._underlying.TryGetValue(binder.Name, out result);
    }

    public override bool TryDeleteMember(DeleteMemberBinder binder)
    {
      return this._underlying.Remove(binder.Name);
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
      this._underlying[binder.Name] = value;
      return true;
    }

    public void Add(KeyValuePair<string, object> item)
    {
      ((ICollection<KeyValuePair<string, object>>) this._underlying).Add(item);
    }

    public void Clear()
    {
      this._underlying.Clear();
    }

    public bool Contains(KeyValuePair<string, object> item)
    {
      return this._underlying.Contains<KeyValuePair<string, object>>(item);
    }

    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      ((ICollection<KeyValuePair<string, object>>) this._underlying).CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, object> item)
    {
      return ((ICollection<KeyValuePair<string, object>>) this._underlying).Remove(item);
    }

    public void Add(string key, object value)
    {
      this._underlying.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
      return this._underlying.ContainsKey(key);
    }

    public bool Remove(string key)
    {
      return this._underlying.Remove(key);
    }

    public bool TryGetValue(string key, out object value)
    {
      return this._underlying.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<string, object>>) this._underlying.GetEnumerator();
    }
  }
}
