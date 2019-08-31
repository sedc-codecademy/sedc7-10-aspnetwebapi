using Newtonsoft.Json;
using SEDC.Loto3000.ApiClient.Contracts;
using SEDC.Loto3000.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Loto3000.ApiClient
{
    public class Loto3000ApiClient : ILoto3000ApiClient
    {
        private readonly string _baseApiAddress;

        public Loto3000ApiClient(string baseApiAddress)
        {
            _baseApiAddress = baseApiAddress;
        }
        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var httpResponse = await CallApiAsync("/user/authenticate", HttpMethod.Post, bodyContent: new { email, password });

            string userToken = null;
            if (httpResponse.Headers.TryGetValues("Authorization", out IEnumerable<string> values))
                userToken = values.FirstOrDefault();

            return userToken;
        }

        public async Task<Draw> CreateDrawAsync(string userToken)
        {
            var headerParams = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {userToken}" }
            };

            var httpResponse = await CallApiAsync("/draw", HttpMethod.Post, headerParams: headerParams);

            string content = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Draw>(content);
        }

        private async Task<HttpResponseMessage> CallApiAsync(string route, HttpMethod httpMethod, Dictionary<string, string> headerParams = null, 
                                    Dictionary<string, string> queryParams = null, object bodyContent = null)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseApiAddress);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string uri = GetUri(route, queryParams);
                using (var httpRequestMessage =
                            new HttpRequestMessage(httpMethod, new Uri(uri)))
                {
                    AddHeaderParams(httpRequestMessage.Headers, headerParams);

                    if (bodyContent != null)
                        httpRequestMessage.Content =
                            new StringContent(JsonConvert.SerializeObject(bodyContent), Encoding.UTF8, "application/json");

                    var httpResponse = await httpClient.SendAsync(httpRequestMessage);
                    httpResponse.EnsureSuccessStatusCode();

                    return httpResponse;
                }
            }
        }

        private string GetUri(string route, Dictionary<string, string> queryParams)
        {
            var result = new StringBuilder($"{_baseApiAddress}{route}");

            if (queryParams?.Count > 0)
            {
                result.Append("?");
                foreach (var paramName in queryParams.Keys)
                    if (!string.IsNullOrWhiteSpace(queryParams[paramName]))
                        result.Append($"{paramName}={queryParams[paramName]}&");

                var lastCharacterIndex = result.Length - 1;
                if (result[lastCharacterIndex] == '?' || result[lastCharacterIndex] == '&')
                    result.Remove(lastCharacterIndex, 1);
            }
            
            return result.ToString();
        }

        private void AddHeaderParams(HttpRequestHeaders headers, Dictionary<string, string> headerParams)
        {
            if (headerParams?.Count > 0)
                foreach (var paramName in headerParams.Keys)
                    if (!string.IsNullOrWhiteSpace(headerParams[paramName]))
                        headers.Add(paramName, headerParams[paramName]);
        }
    }
}
