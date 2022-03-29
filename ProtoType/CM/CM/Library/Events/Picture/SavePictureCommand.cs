using System;
using CM.Library.DataModels;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CM.Library.Events.Picture
{
	public class SavePictureCommand : IRequest<PictureDataModel>
	{
        public IFormFile FormFile { get; set; }

        

        public string PersonId { get; set; }

        public string SubFolderName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <param name="personId"></param>
        /// <param name="subFolderName">The Name of the folder inside the personId Folder. Ex. ProfilePicure</param>
        public SavePictureCommand(IFormFile formFile,  string personId , string subFolderName )
		{
			this.FormFile = formFile;
			
            this.PersonId = personId;
            this.SubFolderName = subFolderName;
		}
	}
}

