using System;
namespace CM.WebClient.Services.Interfaces
{
	public interface IAuthenticationService
	{
		string Token { get; }
		Task Initialize();
		Task Login(string username, string password);
		Task Logout();
	}
}

