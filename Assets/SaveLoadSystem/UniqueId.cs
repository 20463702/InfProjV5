using System;
using UnityEngine;

namespace SaveLoadSystem
{
    [Serializable]
    [ExecuteInEditMode]
    public class UniqueId : MonoBehaviour
    {
        [SerializeField] public static SerializableDictionary<string, GameObject> _idDatabase = new();

        [field: ReadOnly] [SerializeField] public string ID { get; private set; } = Guid.NewGuid().ToString();

        private void OnValidate()
        {
            if (_idDatabase.ContainsKey(ID))
                Generate();
            else
                _idDatabase.Add(ID, gameObject);
        }

        private void OnDestroy()
        {
            if (_idDatabase.ContainsKey(ID))
                _idDatabase.Remove(ID);
        }

        private void Generate()
        {
            ID = Guid.NewGuid().ToString();
            _idDatabase.Add(ID, gameObject);
        }
    }
}
