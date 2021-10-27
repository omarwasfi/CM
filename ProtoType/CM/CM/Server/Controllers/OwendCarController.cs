using CM.Library.DataModels.BusinessModels;
using CM.Library.Queries.OwnedCar;
using CM.Shared.DataViewModels;
using CM.Shared.DataViewModels.BusinessViewModels;
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
    public class OwnedCarController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly PersonController _personController;
        private readonly CarController _CarController;

        public OwnedCarController(IMediator mediator, PersonController personController, CarController carController)
        {
            _mediator = mediator;
            _personController = personController;
            _CarController = carController;
        }

        [HttpGet("GetOwnedCarById")]
        public async Task<IActionResult> GetOwnedCarById(string id)
        {
            try
            {
                OwnedCarDataModel OwnedCarDataModel = await _mediator.Send(new GetOwnedCarByIdQuery(id));

                OwnedCarDataViewModel OwnedCarDataViewModel = new OwnedCarDataViewModel();
                await convertOwnedCarDataModeltoOwnedCarDataViewModel(OwnedCarDataModel, OwnedCarDataViewModel);

                return Ok(OwnedCarDataViewModel);
            }
            catch
            {
                return BadRequest();
            }
        }

        private async Task convertOwnedCarDataModeltoOwnedCarDataViewModel(OwnedCarDataModel OwnedCarDataModel, OwnedCarDataViewModel OwnedCarDataViewModel)
        {
            OwnedCarDataViewModel.Id = OwnedCarDataModel.Id;
            OwnedCarDataViewModel.Name = OwnedCarDataModel.Name;
            OwnedCarDataViewModel.Description = OwnedCarDataViewModel.Description;

            IActionResult personContollerIActionResult = await _personController.GetPersonById(OwnedCarDataModel.Id);
            OkObjectResult personContollerOkObjectResult = personContollerIActionResult as OkObjectResult;
            OwnedCarDataViewModel.Person = personContollerOkObjectResult.Value as PersonDataViewModel;

            IActionResult carControllerActionResult = await _CarController.GetCarById(OwnedCarDataModel.Car.Id);
            OkObjectResult carControllerOkObjectResult = carControllerActionResult as OkObjectResult;
            OwnedCarDataViewModel.Car = carControllerOkObjectResult.Value as CarDataViewModel;
        }
    }
}
