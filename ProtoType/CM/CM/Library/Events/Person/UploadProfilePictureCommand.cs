using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CM.Library.Events.Person
{
	public class UploadProfilePictureCommand : IRequest
	{
        public IFormFileCollection FormFiles { get; set; }

        public string FileExtension { get; set; }

		public string FileName { get; set; }

		public ClaimsPrincipal ClaimsPrincipal { get; set; }



		public UploadProfilePictureCommand(IFormFileCollection formFiles, string fileName, string fileExtension , ClaimsPrincipal claimsPrincipal)
		{
			this.FormFiles = formFiles;
			this.FileName = fileName;
			this.FileExtension = fileExtension;
			this.ClaimsPrincipal = claimsPrincipal;

		}
	}
}

