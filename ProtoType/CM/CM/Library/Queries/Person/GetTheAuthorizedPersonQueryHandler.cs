using CM.Library.DataModels;
using CM.Library.DBContexts;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Library.Queries.Person
{
    public class GetTheAuthorizedPersonQueryHandler : IRequestHandler<GetTheAuthorizedPersonQuery, PersonDataModel>
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        private readonly CurrentStateDBContext _currentStateDBContext;

        /*public GetTheAuthorizedPersonQueryHandler(AuthenticationStateProvider authenticationStateProvider , CurrentStateDBContext currentStateDBContext)
        {
            this._authenticationStateProvider = authenticationStateProvider;
            this._currentStateDBContext = currentStateDBContext;
        }*/

        public async Task<PersonDataModel> Handle(GetTheAuthorizedPersonQuery request, CancellationToken cancellationToken)
        {

            string username = getUser(await getAuthenticationState(this._authenticationStateProvider)).Identity.Name;
            return (PersonDataModel)_currentStateDBContext.Users.Where(x => x.UserName == username);
        }



        private async Task<AuthenticationState> getAuthenticationState(AuthenticationStateProvider authenticationStateProvider)
        {
            return await authenticationStateProvider.GetAuthenticationStateAsync();
        }
        private System.Security.Claims.ClaimsPrincipal getUser(AuthenticationState authenticationState)
        {
            return authenticationState.User;
        }
    }
}
