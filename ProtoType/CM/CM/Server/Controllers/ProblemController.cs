using CM.Library.DataModels.BusinessModels;
using CM.Library.Events.Problem;
using CM.Shared;
using CM.Shared.DataViewModels;
using CM.Shared.DataViewModels.BusinessViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    public class ProblemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProblemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(ProblemDataViewModel problemDataView)
        {
            try
            {
                await _mediator.Send(new AddProblemCommand(
                    problemTypeId: problemDataView.ProblemType.Id,
                    personId: problemDataView.Person.Id,
                    owendCarId: problemDataView.OwendCar.Id,
                    description: problemDataView.Description,
                    location: problemDataView.Location
                    )) ;
                return Ok();
               
            }
            catch
            {
                return BadRequest();
            }

        }

       

      
    }
}
