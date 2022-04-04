using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CM.SharedWithClient;
using CM.WebClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CM.WebClient.Services.Classes
{
	public class HttpService : IHttpService
	{
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }


        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await sendRequest<T>(request);
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequest<T>(request);
        }

        public async Task<T> Put<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequest<T>(request);
        }

        public async Task Post(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            await sendRequest(request);
        }

        public async Task<T> PutBrowseFile<T>(string uri, IBrowserFile browserFile)
        {
            var fileContent = new StreamContent(browserFile.OpenReadStream(maxAllowedSize: 99999999999999));
            fileContent.Headers.ContentType =
                      new MediaTypeHeaderValue(browserFile.ContentType);

            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(content: fileContent, "file", browserFile.Name);
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = form;
            return await sendRequest<T>(request);
           
        }

        // helper methods

        private async Task<T> sendRequest<T>(HttpRequestMessage request)
        {
            // add jwt auth header if user is logged in and request is to the api url
            var token = await _localStorageService.GetItem<TokenDataViewModel>("token");
            var isApiUrl = !request.RequestUri.IsAbsoluteUri;
            if (token != null && isApiUrl)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            
            var response = await _httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }

            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }
            var result = await response.Content.ReadFromJsonAsync<T>();
            return result;
        }

        private async Task sendRequest(HttpRequestMessage request)
        {
            // add jwt auth header if user is logged in and request is to the api url
            var token = await _localStorageService.GetItem<TokenDataViewModel>("token");
            var isApiUrl = !request.RequestUri.IsAbsoluteUri;
            if (token != null && isApiUrl)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

            
            var response = await _httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
            }

            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }
            
        }
    }
}

