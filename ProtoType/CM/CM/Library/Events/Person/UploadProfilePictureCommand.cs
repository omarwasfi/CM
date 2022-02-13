using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CM.Library.Events.Person
{
	public class UploadProfilePictureCommand : IRequest
	{
        public IFormFile FormFile { get; set; }

        public string FileExtension { get; set; }

		public string FileName { get; set; }

		public ClaimsPrincipal ClaimsPrincipal { get; set; }



		public UploadProfilePictureCommand(IFormFile formFile, string fileName, string fileExtension , ClaimsPrincipal claimsPrincipal)
		{
			this.FormFile = formFile;
			this.FileName = fileName;
			this.FileExtension = fileExtension;
			this.ClaimsPrincipal = claimsPrincipal;

		}
	}
}

