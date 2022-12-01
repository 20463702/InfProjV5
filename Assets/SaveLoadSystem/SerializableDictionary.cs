using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace SaveLoadSystem
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [XmlElement("key")] [SerializeField] private List<TKey> keys = new();
        [XmlElement("value")] [SerializeField] private List<TValue> values = new();
 
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach(var pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }
 
        public void OnAfterDeserialize()
        {
            Clear();

            if(keys.Count != values.Count)
                throw new Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

            for(int i = 0; i < keys.Count; i++)
                this.Add(keys[i], values[i]);
        }
    }
}
