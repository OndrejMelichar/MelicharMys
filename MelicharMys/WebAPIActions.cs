using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MelicharMys
{
    public class WebAPIActions
    {
        private string uri = "https://student.sps-prosek.cz/~melicon16/MelicharWebAPI.php";

        public async Task<Profile> LoadProfile(int profileID)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(this.uri + "?profileID=" + profileID);
                string json = await response.Content.ReadAsStringAsync();

                Profile profile = JsonConvert.DeserializeObject<Profile>(json);
                return profile;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public async Task<List<Profile>> LoadAllProfiles()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, this.uri);
            var response = await client.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync(); //hodnota tohoto
            List<Profile> deserializedProfiles = JsonConvert.DeserializeObject<List<Profile>>(responseContent, settings);
            return deserializedProfiles;
        }

        public async Task SaveProfile(Profile profile)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, this.uri);

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("profile", this.serializeProfile(profile)));
            request.Content = new FormUrlEncodedContent(keyValues);
            var response = await client.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync(); //hodnota tohoto
        }


        /* pomocné metody */
        private string serializeProfile(Profile profile)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            string json = JsonConvert.SerializeObject(profile, settings);
            return json;
        }
    }
}
