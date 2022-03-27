using System;
using CM.SharedWithClient;
using CM.WebClient.Services.Interfaces;

namespace CM.WebClient.Services.Classes
{
	public class PersonService : IPersonService
	{
		public PersonService()
		{
		}

        public Task<PersonDataViewModel> GetPerson(string personId)
        {
            throw new NotImplementedException();
        }
    }
}

