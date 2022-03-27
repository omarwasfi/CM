using System;
using CM.WebClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CM.WebClient.Services.Classes
{
	public class AuthenticationService : IAuthenticationService
	{
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        public string Token { get; private set; }


        public AuthenticationService(
              IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
            )
		{
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            Token = await _localStorageService.GetItem<string>("token");
        }

        public async Task Login(string username, string password)
        {
            Token = await _httpService.Post<string>("/users/authenticate", new { username, password });
            await _localStorageService.SetItem("token", Token);
        }

        public async Task Logout()
        {
            Token = "";
            await _localStorageService.RemoveItem("token");
            _navigationManager.NavigateTo("login");
        }
    }
}

