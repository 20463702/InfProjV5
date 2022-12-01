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
    }
}
