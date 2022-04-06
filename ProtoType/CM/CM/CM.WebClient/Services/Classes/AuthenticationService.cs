using System;
using CM.SharedWithClient;
using CM.SharedWithClient.RequestViewModels;
using CM.WebClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CM.WebClient.Services.Classes
{
	public class AuthenticationService : IAuthenticationService
	{
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        public TokenDataViewModel? Token { get; private set; }


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
            Token = await _localStorageService.GetItem<TokenDataViewModel>("token");
        }

        public async Task Login(LoginWithUsernameRequestDataViewModel loginWithUsernameRequest)
        {
            Token = await _httpService.Post<TokenDataViewModel>("Authentication/LoginWithUsername", loginWithUsernameRequest);
            await _localStorageService.SetItem("token", Token);
        }

        public async Task Logout()
        {
            Token = null;
            await _localStorageService.RemoveItem("token");
            _navigationManager.NavigateTo("login");
        }

        public async Task Register(RegisterRequestDataViewModel registerRequest)
        {
            Token = await _httpService.Post<TokenDataViewModel>("Authentication/RegisterWithUsername", registerRequest);
            await _localStorageService.SetItem("token", Token);
        }
    }
}

