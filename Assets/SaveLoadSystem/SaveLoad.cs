using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace SaveLoadSystem
{
    public static class SaveLoad
    {
        public static UnityAction OnSaveGame;
        public static UnityAction<SaveData> OnLoadGame;
        private static readonly string Directory = "/SaveData/";
        private static readonly string FileName = "SaveGame.json";

        private static string FullPath => Application.persistentDataPath + Directory + FileName;

        public static bool Save(SaveData data)
        {
            Debug.LogWarning("Saving game.");
            
            OnSaveGame?.Invoke();
            
            string dir = Application.persistentDataPath + Directory;
            
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(dir + FileName, json);

            return true;
        }

        public static SaveData Load()
        {
            Debug.LogWarning("Loading game.");
            
            SaveData data = new();

            if (File.Exists(FullPath))
            {
                string json = File.ReadAllText(FullPath);
                data = JsonUtility.FromJson<SaveData>(json);
                
                OnLoadGame?.Invoke(data);
            }
            else
                Debug.LogError("Save file does not exist.");

            return data;
        }

        public static void DeleteSaveData()
        {
            Debug.LogWarning("Deleting savedata.");
            
            if (File.Exists(FullPath))
                File.Delete(FullPath);
        }
    }
}
