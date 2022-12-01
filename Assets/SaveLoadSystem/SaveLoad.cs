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
        private static readonly string FileName = "SaveGame.xml";

        private static string FullPath => Application.persistentDataPath + Directory + FileName;

        public static void Save(SaveData data)
        {
            OnSaveGame?.Invoke();

            string dir = Application.persistentDataPath + Directory;
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);
            
            File.WriteAllText(dir + FileName, SaveData.ToXml(data).ToString());

            Debug.LogWarning("Saving game.");
        }

        public static SaveData Load()
        {
            SaveData data = new SaveData();

            if (File.Exists(FullPath))
            {
                var xml = XDocument.Load(FullPath);
                data = SaveData.FromXml(xml);
                
                OnLoadGame?.Invoke(data);
            }
            else
                Debug.LogError("Save file does not exist.");

            return data;
        }

        public static void DeleteSaveData()
        {
            if (File.Exists(FullPath))
                File.Delete(FullPath);
        }
    }
}
