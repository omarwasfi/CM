using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CM.Library.Events.Person
{
	public class UploadProfilePictureCommand : IRequest
	{
        public IFormFile FormFile { get; set; }

        

		public ClaimsPrincipal ClaimsPrincipal { get; set; }



		public UploadProfilePictureCommand(IFormFile formFile,  ClaimsPrincipal claimsPrincipal)
		{
			this.FormFile = formFile;
			
			this.ClaimsPrincipal = claimsPrincipal;

		}
	}
}

