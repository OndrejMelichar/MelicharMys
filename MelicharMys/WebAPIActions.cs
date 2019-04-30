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
        private string uri = "https://api.chucknorris.io/jokes/random";

        public async Task<Profile> LoadProfile(int profileID)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(this.uri);
            string json = await response.Content.ReadAsStringAsync();

            Profile profile = JsonConvert.DeserializeObject<Profile>(json);
            return profile;
        }

        public async Task<List<Profile>> LoadAllProfiles()
        {
            return new List<Profile>();
        }

        public async Task SaveProfile(Profile profile)
        {

        }
    }
}
