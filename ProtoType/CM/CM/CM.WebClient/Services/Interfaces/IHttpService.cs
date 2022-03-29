using System;
using Microsoft.AspNetCore.Components.Forms;

namespace CM.WebClient.Services.Interfaces
{
	public interface IHttpService
	{
		Task<T> Get<T>(string uri);
		Task<T> Post<T>(string uri, object value);
		Task<T> Put<T>(string uri, object value);
		Task Post(string uri, object value);
		Task<T> PutBrowseFile<T>(string uri, IBrowserFile browserFile);

	}
}

