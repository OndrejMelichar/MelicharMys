using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelicharMys
{
    public class JsonActions
    {
        private string fileName;

        public JsonActions(string fileName)
        {
            this.fileName = fileName;
        }

        public List<Profile> LoadProfiles()
        {
            if (File.Exists(fileName))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };

                string jsonFromFile = File.ReadAllText(fileName);
                List<Profile> deserializedProfiles = JsonConvert.DeserializeObject<List<Profile>>(jsonFromFile, settings);
                return deserializedProfiles;
            }

            return null;
        }

        public void SaveProfiles(List<Profile> profiles)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            string json = JsonConvert.SerializeObject(profiles, settings);
            File.WriteAllText(fileName, json);
        }
    }
}
