using System;
using System.Security.Claims;
using MediatR;

namespace CM.Library.Events.Person
{
	public class UpdateProfileCommand : IRequest
	{
        public ClaimsPrincipal ClaimsPrincipal { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }


        public UpdateProfileCommand(ClaimsPrincipal claimsPrincipal, string firstName, string lastName, string gender)
        {
            ClaimsPrincipal = claimsPrincipal;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;

        }
    }
}

