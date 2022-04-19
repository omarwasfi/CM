using System;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels;
using CM.Library.DataModels.Events;
using CM.Library.DBContexts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;


namespace CM.Library.Events.Token
{
	public class CreateTokenByUserNameCommandHandler : IRequestHandler<CreateTokenByUserNameCommand, string>
	{
		private readonly EventsDBContext _eventsDBContext;
        private readonly CurrentStateDBContext _currentStateDBContext;
        private readonly UserManager<PersonDataModel> _userManager;


        public CreateTokenByUserNameCommandHandler(EventsDBContext eventsDBContext, CurrentStateDBContext currentStateDBContext , UserManager<PersonDataModel> userManager)
		{
			this._eventsDBContext = eventsDBContext;
            this._currentStateDBContext = currentStateDBContext;
            this._userManager = userManager;
		}

        public async Task<string> Handle(CreateTokenByUserNameCommand request, CancellationToken cancellationToken)
        {
            EventDataModel generateTokenEvent = new EventDataModel();

            generateTokenEvent.Type = EventType.GenerateToken;
            generateTokenEvent.DateTime = DateTime.Now;
            generateTokenEvent.Content = JsonConvert.SerializeObject(request);

            await _eventsDBContext.Events.AddAsync(generateTokenEvent);
            await _eventsDBContext.SaveChangesAsync();


            return await applyEventToTheCurrentState(request);

        }

        private async Task<string> applyEventToTheCurrentState(CreateTokenByUserNameCommand request )
        {
            PersonDataModel user = await _userManager.FindByNameAsync(request.UserName);

            var userRoles = _currentStateDBContext.UserRoles.Where(x=>x.UserId == user.Id).ToList();
            var roles = new List<IdentityRole>();
            foreach(var  identityUserRole in userRoles)
            {
                var role = await _currentStateDBContext.Roles.FindAsync(identityUserRole.RoleId);
                roles.Add(role);
            }

            
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(7)).ToUnixTimeSeconds().ToString())

            };

            if(user.UserName != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            }
            if (user.PhoneNumber != null)
            {
                claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));

            }
            if (user.Email != null)
            {
                claims.Add(new Claim(ClaimTypes.Email, user.Email));

            }


            foreach (var identityRole in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, identityRole.Name));
            }

            var token = new JwtSecurityToken
           (
               //issuer: "https://localhost:7093/",
               //audience: "https://localhost:7093/",
               issuer: request.Issuer,
               audience: request.Audience,
               claims: claims,
               expires: DateTime.UtcNow.AddDays(7),
               notBefore: DateTime.UtcNow,
               signingCredentials: new SigningCredentials(
                   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(request.JwtKey)),
                   SecurityAlgorithms.HmacSha256)
           );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}

