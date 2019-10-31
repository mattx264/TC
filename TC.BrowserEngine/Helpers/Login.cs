﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace TC.BrowserEngine.Helpers
{

    public static class Login
    {
        static HttpClient client = new HttpClient();


        public static async Task<string> LoginAsync(string email, string password)
        {
            try
            {
                string authUrl;

                authUrl = ConfigHelper.GetServerAddress() + "api/User/CreateToken";

                LoginViewModel loginViewModel = new LoginViewModel()
                {
                    Email = email,
                    Password = password
                };
                var json = JsonConvert.SerializeObject(loginViewModel);

                var loginData = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                loginData.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync(
                    authUrl, loginData).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                string output = response.Content.ReadAsStringAsync().GetAwaiter().GetResult(); //right!
                var data = JsonConvert.DeserializeObject<LoginResponse>(output);
                data.Token = data.Token;
                // return URI of the created resource.
                return data.Token;// response.Content
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public class LoginViewModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class LoginResponse
        {
            public string Name { get; set; }
            public string Token { get; set; }
        }
    }
}
