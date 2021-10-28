using CM.Library.DataModels.BusinessModels;
using CM.Library.Events.Problem;
using CM.Library.Queries.Problem;
using CM.Library.Queries.ProblemType;
using CM.Shared;
using CM.Shared.DataViewModels;
using CM.Shared.DataViewModels.BusinessViewModels;
using FluentValidation;
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
        private readonly ProblemTypeController _problemTypeController;
        private readonly PersonController _personController;
        private readonly OwnedCarController _OwnedCarController;


        public ProblemController(IMediator mediator, ProblemTypeController problemTypeController, PersonController personController, OwnedCarController OwnedCarController)
        {
            _mediator = mediator;
            _problemTypeController = problemTypeController;
            _personController = personController;
            _OwnedCarController = OwnedCarController;
        }

        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(ProblemDataViewModel problemDataView)
        {
            try
            {
                ProblemDataModel newProblemDataModel =  await _mediator.Send(new AddProblemCommand(
                    problemTypeId: problemDataView.ProblemType.Id,
                    personId: problemDataView.Person.Id,
                    ownedCarId: problemDataView.OwnedCar.Id,
                    description: problemDataView.Description,
                    location: problemDataView.Location
                    )) ;
                return Ok(newProblemDataModel.Id);
               
            }
            catch(ValidationException v)
            {
                return ValidationProblem(v.Message);
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
                List<ProblemDataModel> problemDataModels = await _mediator.Send(new GetProblemsQuery());
                List<ProblemDataViewModel> problemDataViewModels = new List<ProblemDataViewModel>();
                await convertProblemDataModelToProblemDataViewModels(problemDataModels, problemDataViewModels);
                return Ok(problemDataViewModels);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("GetProblemsByPersonId")]
        public async Task<IActionResult> GetProblemsByPersonId(string id)
        {
            try
            {
                List<ProblemDataModel> problemDataModels = await _mediator.Send(new GetProblemsByPersonIdQuery(id));
                List<ProblemDataViewModel> problemDataViews = new List<ProblemDataViewModel>();

                await convertProblemDataModelToProblemDataViewModels(problemDataModels, problemDataViews);



                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        private async Task convertProblemDataModelToProblemDataViewModels(List<ProblemDataModel> problemDataModels, List<ProblemDataViewModel> problemDataViewModels)
        {
            foreach (ProblemDataModel problem in problemDataModels)
            {
                ProblemDataViewModel problemDataView = await ConvertProblemDataModelToProblemDataViewModel(problem);

                problemDataViewModels.Add(problemDataView);
            }
        }

        private async Task<ProblemDataViewModel> ConvertProblemDataModelToProblemDataViewModel(ProblemDataModel problem)
        {
            ProblemDataViewModel problemDataView = new ProblemDataViewModel();

            problemDataView.Id = problem.Id;

            IActionResult problemTypeControllerActionResult = await _problemTypeController.GetProblemTypeById(problem.ProblemType.Id);
            OkObjectResult problemTypeControlleOkObjectResult = problemTypeControllerActionResult as OkObjectResult;
            problemDataView.ProblemType = problemTypeControlleOkObjectResult.Value as ProblemTypeDataViewModel;

            problemDataView.State = problem.State.ToString();

            IActionResult personControllerActionResult = await _personController.GetPersonById(problem.Person.Id);
            OkObjectResult pesonControllerOkObjectResult = personControllerActionResult as OkObjectResult;
            problemDataView.Person = pesonControllerOkObjectResult.Value as PersonDataViewModel;

            IActionResult OwnedCarControllerActionResult = await _OwnedCarController.GetOwnedCarById(problem.OwnedCar.Id);
            OkObjectResult OwnedCarControllerOkObjectResult = OwnedCarControllerActionResult as OkObjectResult;
            problemDataView.OwnedCar = OwnedCarControllerOkObjectResult.Value as OwnedCarDataViewModel;

            problemDataView.Description = problem.Description;
            problemDataView.DateTime = problem.DateTime;
            problemDataView.Location = problem.Location;
            return problemDataView;
        }
    }
}
