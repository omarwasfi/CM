using System;
using System.IdentityModel.Tokens.Jwt;
using MediatR;

namespace CM.Library.Events.Token
{
	public class CreateTokenByUserNameCommand : IRequest<string>
	{
        public string UserName { get; set; }
        public string Issuer { get; set; }
		public string Audience { get; set; }
		public string JwtKey { get; set; }


		public CreateTokenByUserNameCommand(string username, string issuer,string audience,string jwtKey)
		{
			this.UserName = username;
			this.Issuer = issuer;
			this.Audience = audience;
			this.JwtKey = jwtKey;
		}
	}
}

