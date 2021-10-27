using CM.Library.DataModels.BusinessModels;
using CM.Library.Events.CarBrand;
using CM.Library.Queries.CarBrand;
using CM.Shared.DataViewModels.BusinessViewModels;
using FluentValidation;
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
    public class CarBrandContoller : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarBrandContoller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddNewCarBrand")]
        public async Task<IActionResult> AddNewCarBrand(CarBrandDataViewModel carBrandDataViewModel)
        {
            try
            {
                CarBrandDataModel carBrandDataModel = await _mediator.Send(new AddNewCarBrandCommand(carBrandDataViewModel.Name, carBrandDataViewModel.Description));
                return Ok(carBrandDataModel.Id);
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

        [HttpGet("GetCarBrandById")]
        public async Task<IActionResult> GetCarBrandById(string id)
        {
            try
            {
                CarBrandDataModel carBrandDataModel = await _mediator.Send(new GetCarBrandByIdQuery(id));
                CarBrandDataViewModel carBrandDataView = new CarBrandDataViewModel();
                ConvertCarBrandDataModelToCarBrandDataViewModel(carBrandDataModel, carBrandDataView);

                return Ok(carBrandDataView);
            }
            catch
            {
                return BadRequest();
            }
        }

        private static void ConvertCarBrandDataModelToCarBrandDataViewModel(CarBrandDataModel carBrandDataModel, CarBrandDataViewModel carBrandDataView)
        {
            carBrandDataView.Id = carBrandDataModel.Id;
            carBrandDataView.Name = carBrandDataModel.Name;
            carBrandDataView.Description = carBrandDataModel.Description;
        }
    }
}
