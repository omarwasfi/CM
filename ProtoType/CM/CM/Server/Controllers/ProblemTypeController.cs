using CM.Library.DataModels.BusinessModels;
using CM.Library.Events.ProblemType;
using CM.Library.Queries.ProblemType;
using CM.Shared.DataViewModels.BusinessViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
    public class ProblemTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProblemTypeController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [Authorize]
        [HttpPost("Add")]

        public async Task<IActionResult> Add(ProblemTypeDataViewModel problemTypeDataView)
        {
            try
            {
                await _mediator.Send(new AddProblemTypeCommand(problemTypeDataView.Name));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
            
        }

        [Authorize]
        [HttpGet("Get")]

        public async Task<List<ProblemTypeDataViewModel>> GetAsync()
        {
            try
            {
                
                List<ProblemTypeDataModel> problemTypeDataModels = await _mediator.Send(new GetProblemTypesQuery());

                List<ProblemTypeDataViewModel> problemTypeDataViewModels = new List<ProblemTypeDataViewModel>();

                foreach(ProblemTypeDataModel problemType in problemTypeDataModels)
                {
                    ProblemTypeDataViewModel problemTypeDataViewModel = new ProblemTypeDataViewModel();
                    problemTypeDataViewModel.Id = problemType.Id;
                    problemTypeDataViewModel.Name = problemType.Name;

                    problemTypeDataViewModels.Add(problemTypeDataViewModel);
                }

                return problemTypeDataViewModels;
            }
            catch
            {
                return null;
            }
        }
    }
}
