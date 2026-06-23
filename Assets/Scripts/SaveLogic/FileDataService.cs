using FullSerializer;
using GlobalDataTypes;
using System.IO;
using UnityEngine;

namespace System.Persistence
{


    public class FileDataService : ISaveable
    {
        ISerializer serializer;
        string dataPath;
        string fileExtension;

        public FileDataService(ISerializer new_serializer)
        {
            this.dataPath = Application.persistentDataPath;
            this.fileExtension = "json";
            this.serializer = new_serializer;
        }

        string GetPathToFile(string filename)
        {
            return Path.Combine(dataPath, string.Concat(filename, ".", fileExtension));
        }
        public GameData LoadAndSetData(string identifier)
        {
            string file_location = GetPathToFile(identifier);
            if (!File.Exists(file_location))
            {
                return null;
            }
            return serializer.Deserialize<GameData>(File.ReadAllText(file_location));
        }

        public void SaveData(GameData data, bool should_overwrite = false)
        {

            string file_location = GetPathToFile(data.identifier);
            
            if(!should_overwrite && File.Exists(file_location))
            {
                return;
            }

            File.WriteAllText(file_location, serializer.Serialize(data));
        }
        public void DeleteSave(string identifier)
        {
            string file_location = GetPathToFile(identifier);
            if (!File.Exists(file_location))
            {
                return;
            }
            File.Delete(identifier);
        }
    }
}
