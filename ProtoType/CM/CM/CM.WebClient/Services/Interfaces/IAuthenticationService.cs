using System;
using CM.SharedWithClient;
using CM.SharedWithClient.RequestViewModels;

namespace CM.WebClient.Services.Interfaces
{
	public interface IAuthenticationService
	{
		TokenDataViewModel? Token { get; }
		Task Initialize();
		Task Login(LoginWithUsernameRequestDataViewModel loginWithUsernameRequest);
		Task Register(RegisterRequestDataViewModel registerRequest);
		Task Logout();
	}
}

