using System;
using System.Security.Claims;
using AutoMapper;
using CM.Library.DataModels;
using CM.Library.Events.Person;
using CM.Library.Queries.Person;
using CM.Library.Queries.Picture;
using CM.SharedWithClient;
using CM.SharedWithClient.RequestViewModels;
using FluentValidation;
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
        [HttpGet]
        [Route("GetTheAuthorizedPerson")]
        public async Task<ActionResult<PersonDataViewModel>> GetTheAuthorizedPerson()
        {


            try
            {
                PersonDataModel person = await _mediator.Send(new GetTheAuthorizedPersonQuery(this.User));

                PersonDataViewModel personDataViewModel = new PersonDataViewModel();

                personDataViewModel = _mapper.Map<PersonDataViewModel>(person);

           

                return Ok(personDataViewModel) ;
            }

            catch (ValidationException v)
            {
                return ValidationProblem(v.Message);
            }
            catch
            {
                return BadRequest("Unrecognized error");

            }
        }

        /// <summary>
        /// Expected to get MultipartFormDataContent in the HttpContent
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UploadProfilePicture")]
        public async Task<ActionResult<PictureBase64DataViewModel>> UploadProfilePicture( IFormFile file)
        {

           

            try
            {
                 await _mediator.Send(new UploadProfilePictureCommand(file, this.User));

                PersonDataModel person = await _mediator.Send(new GetTheAuthorizedPersonQuery(this.User));

                PictureBase64DataViewModel base64DataViewModel = _mapper.Map<PictureBase64DataViewModel>(person.ProfilePicture);

                return Ok(base64DataViewModel);
            }
            catch (ValidationException v)
            {
                return ValidationProblem(v.Message);
            }
            catch
            {
                return BadRequest("Unrecognized error");

            }

        }

        [HttpPut]
        [Route("UpdateProfile")]
        public async Task<ActionResult> UpdateProfile(PersonUpdateProfileRequestViewModel personUpdateProfileRequestViewModel)
        {
            
            try
            {
                await _mediator.Send(new UpdateProfileCommand(
                                this.User,personUpdateProfileRequestViewModel.FirstName,
                                personUpdateProfileRequestViewModel.LastName,
                                personUpdateProfileRequestViewModel.Gender));

                return Ok();
            }
            catch (ValidationException v)
            {
                return ValidationProblem(v.Message);
            }
            catch
            {
                return BadRequest("Unrecognized error");

            }
        }

        [HttpGet]
        [Route("GetContacts")]
        public async Task<ActionResult<List<PersonDataViewModel>>> GetContacts()
        {


            try
            {
                List<PersonDataModel> people = await _mediator.Send(new GetContactsQuery(this.User));

                List<PersonDataViewModel> PersonDataViewModels = new List<PersonDataViewModel>();

                PersonDataViewModels = _mapper.Map<List<PersonDataViewModel>>(people);



                return Ok(PersonDataViewModels);
            }

            catch (ValidationException v)
            {
                return ValidationProblem(v.Message);
            }
            catch
            {
                return BadRequest("Unrecognized error");

            }
        }
    }
}

