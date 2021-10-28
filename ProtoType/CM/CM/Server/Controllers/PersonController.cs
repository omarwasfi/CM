using CM.Library.DataModels;
using CM.Library.Queries.Person;
using CM.Shared.DataViewModels;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetPersonById")]
        public async Task<IActionResult> GetPersonById(string id)
        {
            try
            {
                PersonDataModel personDataModel = await _mediator.Send(new GetPersonByIdQuery(id));

                PersonDataViewModel personDataViewModel = new PersonDataViewModel();
                ConvertPersonDataModelToPersonDataViewModel(personDataModel, personDataViewModel);
                return Ok(personDataViewModel);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        private static void ConvertPersonDataModelToPersonDataViewModel(PersonDataModel personDataModel, PersonDataViewModel personDataViewModel)
        {
            personDataViewModel.Id = personDataModel.Id;
            personDataViewModel.UserName = personDataModel.UserName;
            personDataViewModel.FirstName = personDataModel.FirstName;
            personDataViewModel.LastName = personDataModel.LastName;
            personDataViewModel.IsAFixer = personDataModel.IsAFixer;
        }
    }
}
