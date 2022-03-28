using System;
using CM.SharedWithClient;
using CM.WebClient.Services.Interfaces;

namespace CM.WebClient.Services.Classes
{
	public class PersonService : IPersonService
	{
        private IHttpService _httpService;

        public PersonService(IHttpService httpService)
		{
            this._httpService = httpService;
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
    }
}

