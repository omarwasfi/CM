using System;
using System.Security.Claims;
using AutoMapper;
using CM.Library.DataModels;
using CM.Library.Queries.Person;
using CM.Library.Queries.Picture;
using CM.SharedWithClient;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CM.API.Controllers
{
	[ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class PersonController : ControllerBase
	{
        private IMediator _mediator { get; set; }
        private readonly IMapper _mapper;

        public PersonController(IMediator mediator , IMapper mapper)
		{
            this._mediator = mediator;
            this._mapper = mapper;
		}

        /// <summary>
        /// Getting the person information with his profile picure in base64
        /// If his picrure is not exists the return will be the default picture
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetPerson")]
        public async Task<ActionResult<PersonDataViewModel>> GetPerson()
        {
            PersonDataModel person = await _mediator.Send(new GetTheAuthorizedPersonQuery(this.User));

            PersonDataViewModel personDataViewModel = new PersonDataViewModel();

            personDataViewModel = _mapper.Map<PersonDataViewModel>(person);

            personDataViewModel.ProfilePicture = new PicureBase64DataViewModel()
            {
                Base64 = await _mediator.Send(new GetPictureAsBase64Query(person.ProfilePicture))
            };

            return Ok(personDataViewModel) ;
        }
    }
}

