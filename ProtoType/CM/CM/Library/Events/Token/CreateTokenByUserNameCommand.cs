using System;
using System.IdentityModel.Tokens.Jwt;
using MediatR;

namespace CM.Library.Events.Token
{
	public class CreateTokenByUserNameCommand : IRequest<string>
	{
        public string UserName { get; set; }
        public CreateTokenByUserNameCommand(string username)
		{
			this.UserName = username;
		}
	}
}

