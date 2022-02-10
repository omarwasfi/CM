using CM.Library.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Queries.Person
{
    public class GetTheAuthorizedPersonQuery : IRequest<PersonDataModel>
    {
        public  ClaimsPrincipal ClaimsPrincipal { get; set; }
        public GetTheAuthorizedPersonQuery(ClaimsPrincipal claimsPrincipal)
        {
            this.ClaimsPrincipal = claimsPrincipal;
        }

    }
}
