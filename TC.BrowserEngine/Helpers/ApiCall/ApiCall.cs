using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TC.BrowserEngine.Helpers.ApiCall
{
   
    public class ApiCall<T> where T : class
    {
        private HttpClient client = new HttpClient();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="suffixUrl"></param> "api/User/CreateToken"
        /// <returns></returns>
        public async Task<T> GetAsync(string suffixUrl,string token)
        {
            string url = ConfigHelper.GetServerAddress() + suffixUrl;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string output = response.Content.ReadAsStringAsync().GetAwaiter().GetResult(); //right!
            var data = JsonConvert.DeserializeObject<T>(output);
            return data;
        }
    }
}
