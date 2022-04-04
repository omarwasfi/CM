using System;
using CM.SharedWithClient;
using CM.WebClient.Services.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace CM.WebClient.Services.Classes
{
	public class PersonService : IPersonService
	{
        private IHttpService _httpService;

        public PersonService(IHttpService httpService)
		{
            this._httpService = httpService;
		}

        public async Task<List<PersonDataViewModel>> GetContacts()
        {
            List<PersonDataViewModel> personDataViewModels = new List<PersonDataViewModel>();
            personDataViewModels = await _httpService.Get<List<PersonDataViewModel>>("Person/GetContacts");

            return personDataViewModels;
        }

        public Task<PersonDataViewModel> GetPerson(string personId)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonDataViewModel> GetTheAuthorizedPerson()
        {
            PersonDataViewModel person = await _httpService.Get<PersonDataViewModel>("Person/GetTheAuthorizedPerson");

            return person;
        }

        public async Task<PictureBase64DataViewModel> UpdateProfilePicture(IBrowserFile browserFile)
        {
            return await _httpService.PutBrowseFile<PictureBase64DataViewModel>("Person/UploadProfilePicture", browserFile);
        }
    }
}

