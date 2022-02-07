using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        public IMediator _mediator { get; set; }

        public TokenController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(string username , string password)
        {
            try
            {

            }
            catch (ValidationException v)
            {
                return ValidationProblem(v.Message);
            }
            catch
            {
                return BadRequest("Unrecognized error ");

            }
            return Ok();
        }

    }
}

