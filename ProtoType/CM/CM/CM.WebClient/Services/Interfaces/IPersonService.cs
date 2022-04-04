using System;
using CM.SharedWithClient;
using Microsoft.AspNetCore.Components.Forms;

namespace CM.WebClient.Services.Interfaces
{
	public interface IPersonService
	{
		public Task<PersonDataViewModel> GetPerson(string personId);
		public Task<PersonDataViewModel> GetTheAuthorizedPerson();
		public Task<PictureBase64DataViewModel> UpdateProfilePicture(IBrowserFile browserFile);
		public Task<List<PersonDataViewModel>> GetContacts();
	}
}

