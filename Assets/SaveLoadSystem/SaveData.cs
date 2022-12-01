using System.Xml.Linq;
using Items.Inventory.Chest;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveLoadSystem
{
    public class SaveData
    {
        public SerializableDictionary<string, ChestSaveData> chestDictionary;

        public SaveData()
        {
            chestDictionary = new();
        }

        public static XDocument ToXml(SaveData d) =>
            JsonConvert.DeserializeXNode(JsonUtility.ToJson(d, true));

        public static SaveData FromXml(XDocument xml) =>
            JsonUtility.FromJson<SaveData>(JsonConvert.SerializeXNode(xml));
    }
}
