using System;
using MediatR;

namespace CM.Library.Events.Token
{
	public class CreateTokenCommand : IRequest<string>
	{
		public string Usernamme { get; set; }
		public string Password { get; set; }


		public CreateTokenCommand(string usernamme, string password)
		{
			Usernamme = usernamme;
			Password = password;
		}
	}
}

