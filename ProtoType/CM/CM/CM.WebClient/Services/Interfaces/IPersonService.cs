using System;
using CM.SharedWithClient;
namespace CM.WebClient.Services.Interfaces
{
	public interface IPersonService
	{
		public Task<PersonDataViewModel> GetPerson(string personId);
	}
}

