using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;
using WebApiClient.Contracts;

namespace WebApiClient
{
    public class NotesWebApiClient : INotesWebApiClient
    {
        private readonly string _baseApiUrl;

        public NotesWebApiClient(string baseApiUrl)
        {
            _baseApiUrl = baseApiUrl;
        }
        public async Task<UserModel> AuthenticateAsync(string username, string password)
        {
            var bodyRequest = new { username, password };
            var httpResponse = await CallApiAsync("api/users/authenticate", HttpMethod.Post, bodyContent: bodyRequest);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserModel>(responseContent);
        }

        public async Task<IEnumerable<NoteModel>> GetNotesByUserAsync(string userToken)
        {
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {userToken}" }
            };
            var httpResponse = await CallApiAsync("api/notes", HttpMethod.Get, headers: headers);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<NoteModel>>(responseContent);
        }

        private async Task<HttpResponseMessage> CallApiAsync(string route, HttpMethod httpMethod, 
                            Dictionary<string, string> headers = null, object bodyContent = null)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var httpRequest = new HttpRequestMessage(httpMethod, $"{_baseApiUrl}{route}"))
                {
                    if (bodyContent != null)
                    {
                        var stringContent = JsonConvert.SerializeObject(bodyContent);
                        httpRequest.Content = new StringContent(stringContent, Encoding.UTF8, "application/json");
                    }
                    if (headers != null)
                        SetHeaders(httpRequest.Headers, headers);

                    var httpResponse = await httpClient.SendAsync(httpRequest);
                    httpResponse.EnsureSuccessStatusCode();

                    return httpResponse;
                }
            }
        }

        private void SetHeaders(HttpRequestHeaders requestHeaders, Dictionary<string, string> headers)
        {
            foreach (var item in headers)
                requestHeaders.Add(item.Key, item.Value);
        }
    }
}
