using CM.Library.DataModels;
using CM.Library.DBContexts;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Library.Queries.Person
{
    public class GetTheAuthorizedPersonQueryHandler : IRequestHandler<GetTheAuthorizedPersonQuery, PersonDataModel>
    {

        private readonly CurrentStateDBContext _currentStateDBContext;

        public GetTheAuthorizedPersonQueryHandler( CurrentStateDBContext currentStateDBContext)
        {
            this._currentStateDBContext = currentStateDBContext;
        }

        public async Task<PersonDataModel> Handle(GetTheAuthorizedPersonQuery request, CancellationToken cancellationToken)
        {
            string userId = getTheUseId(request.ClaimsPrincipal);

            PersonDataModel person = await _currentStateDBContext.Users.FindAsync(userId);

            return person;
        }

        private string getTheUseId(ClaimsPrincipal claimsPrincipal)
        {
            ClaimsIdentity claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

    }
}
