using System;
using System.Collections.Generic;
using System.Security.Claims;
using CM.Library.DataModels;
using MediatR;

namespace CM.Library.Queries.Person
{
	public class GetContactsQuery : IRequest<List<PersonDataModel>>
	{
		public ClaimsPrincipal ClaimsPrincipal { get; set; }

		

        public GetContactsQuery(ClaimsPrincipal claimsPrincipal)
        {
            ClaimsPrincipal = claimsPrincipal;
        }
    }
}

