using System;
using System.Collections.Generic;
using System.Text;

namespace TC.BrowserEngine.Helpers
{
    public class TokenAuth
    {

        //private static async Task<string> GetToken()
        //{
        //    string clientId = "YOUR CLIENT ID";
        //    string clientSecret = "YOUR CLIENT SECRET";
        //    string credentials = String.Format("{0}:{1}", clientId, clientSecret);

        //    using (var client = new HttpClient())
        //    {
        //        //Define Headers
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

        //        //Prepare Request Body
        //        List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
        //        requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

        //        FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

        //        //Request Token
        //        var request = await client.PostAsync("https://accounts.spotify.com/api/token", requestBody);
        //        var response = await request.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<AccessToken>(response);
        //    }
        //}
    }

}
