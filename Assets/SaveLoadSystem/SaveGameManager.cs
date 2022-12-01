using UnityEngine;
using UnityEngine.UI;

namespace SaveLoadSystem
{
    public class SaveGameManager : MonoBehaviour
    {
        public static SaveData data;

        private void Awake()
        {
            data = new();
            SaveLoad.OnLoadGame += LoadData;
        }

        public void DeleteData()
        {
            SaveLoad.DeleteSaveData();
        }
        
        public static void SaveData()
        {
            SaveLoad.Save(data);
        }
        
        public static void LoadData(SaveData d)
        {
            data = d;
        }

        public static void TryLoadData()
        {
            SaveLoad.Load();
        }
    }
}
