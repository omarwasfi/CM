using CM.Library.DataModels;
using CM.Shared.DataViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using CM.Library.Events.Person;
using FluentValidation;
using Microsoft.AspNetCore.Cors;

namespace CM.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<PersonDataModel> _userManager;
        private readonly SignInManager<PersonDataModel> _signInManager;
        private readonly IMediator _mediator;
        public AuthController(UserManager<PersonDataModel> userManager, SignInManager<PersonDataModel> signInManager , IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                await _mediator.Send(new LoginPersonCommand(request.UserName, request.Password, request.RememberMe));
                return Ok();
            }
            catch(ValidationException v)
            {
                return ValidationProblem(v.Message);
            }
            catch
            {
                return BadRequest("Unrecognized error ");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest parameters)
        {
            
            var user = new PersonDataModel();
            user.UserName = parameters.UserName;

            try{
                await _mediator.Send(new RegisterPersonCommand(user , parameters.Password));
               
                return await Login(new LoginRequest
                {
                    UserName = parameters.UserName,
                    Password = parameters.Password
                });
            }
            catch(ValidationException v)
            {
                return ValidationProblem(v.Message);
            }
            catch
            {
                return BadRequest("Unrecognized error ");
            }
                       
        }   


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _mediator.Send(new LogoutPersonCommand(User.Identity.Name));
                return Ok();

            }
            catch
            {
                return BadRequest("Unrecognized error ");

            }
        }

        [HttpGet]
        public CurrentUser GetCurrentUserInfo()
        {
            return new CurrentUser
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                Claims = User.Claims
                .ToDictionary(c => c.Type, c => c.Value)
            };
        }
    }
}
