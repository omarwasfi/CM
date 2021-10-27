using CM.Library.DataModels.BusinessModels;
using CM.Library.Queries.Car;
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
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CarBrandContoller _CarBrandController;

        public CarController(IMediator mediator , CarBrandContoller carBrandContoller)
        {
            _mediator = mediator;
            _CarBrandController = carBrandContoller;
        }

        [HttpGet("GetCarById")]
        public async Task<IActionResult> GetCarById(string id)
        {
            try
            {
                CarDataModel carDataModel = await _mediator.Send(new GetCarByIdQuery(id));
                CarDataViewModel carDataViewModel = new CarDataViewModel();
                await CovertCarDataModelToCarDataViewModel(carDataModel, carDataViewModel);

                return Ok(carDataViewModel);
            }
            catch
            {

                return BadRequest();
            }
        }

        private async Task CovertCarDataModelToCarDataViewModel(CarDataModel carDataModel, CarDataViewModel carDataViewModel)
        {
            carDataViewModel.Id = carDataModel.Id;
            carDataViewModel.Name = carDataModel.Name;
            carDataViewModel.Description = carDataModel.Description;

            IActionResult carBrandControllerIActionResult = await _CarBrandController.GetCarBrandById(carDataViewModel.CarBrand.Id);
            OkObjectResult carBrandControllerOkObjectResult = carBrandControllerIActionResult as OkObjectResult;
            carDataViewModel.CarBrand = carBrandControllerOkObjectResult.Value as CarBrandDataViewModel;
        }
    }
}
