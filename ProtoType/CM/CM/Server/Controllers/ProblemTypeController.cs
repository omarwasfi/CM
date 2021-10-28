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
        [HttpPost("Add")]

        public async Task<IActionResult> Add(ProblemTypeDataViewModel problemTypeDataView)
        {
            try
            {
                ProblemTypeDataModel problemTypeDataModel =  await _mediator.Send(new AddProblemTypeCommand(problemTypeDataView.Name));
                return Ok(problemTypeDataModel.Id);
            }
            catch
            {
                return BadRequest();
            }


        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            try
            {

                List<ProblemTypeDataModel> problemTypeDataModels = await _mediator.Send(new GetProblemTypesQuery());

                List<ProblemTypeDataViewModel> problemTypeDataViewModels = new List<ProblemTypeDataViewModel>();

                foreach (ProblemTypeDataModel problemType in problemTypeDataModels)
                {
                    ProblemTypeDataViewModel problemTypeDataViewModel = new ProblemTypeDataViewModel();
                    problemTypeDataViewModel.Id = problemType.Id;
                    problemTypeDataViewModel.Name = problemType.Name;

                    problemTypeDataViewModels.Add(problemTypeDataViewModel);
                }

                return Ok(problemTypeDataViewModels);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetProblemTypeById")]
        public async Task<IActionResult> GetProblemTypeById(string id)
        {
            try
            {
                ProblemTypeDataModel problemTypeDataModel = await _mediator.Send(new GetProblemTypeByIdQuery(id));

                ProblemTypeDataViewModel problemTypeDataViewModel = new ProblemTypeDataViewModel();

                problemTypeDataViewModel.Id = problemTypeDataModel.Id;
                problemTypeDataViewModel.Name = problemTypeDataModel.Name;

                return Ok(problemTypeDataViewModel);
            }
            catch
            {
                return BadRequest();
            }
            
        }
    }
}
