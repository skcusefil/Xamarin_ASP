﻿using clientXamarin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace clientXamarin.Services
{
    class MemberService
    {
        private readonly static string localHost = "https://192.168.178.25:45455/api/user/";

        public static async Task<Member> GetMembers(string username)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            var httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));

            var response = await httpClient.GetStringAsync(localHost+username);

            if (response == null)
            {
                await App.Current.MainPage.DisplayAlert("Notfound", "can not find user", "Cancel");
            }

            var result = JsonConvert.DeserializeObject<Member>(response);

            return result;
        }
    }
}
