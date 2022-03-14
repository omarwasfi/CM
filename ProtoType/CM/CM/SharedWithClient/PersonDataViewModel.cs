using System;
namespace CM.SharedWithClient
{
	public class PersonDataViewModel
	{
        public string? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Gender { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public bool? EmailConfirmed { get; set; }

        public string? PhoneNumber { get; set; }

        public bool? PhoneNumberConfirmed { get; set; }

        public PictureBase64DataViewModel? ProfilePicture { get; set; }


    }
}

