using CM.Library.DataModels;
using CM.Shared.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using CM.Library.Events.Person;

namespace CM.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return BadRequest("User does not exist");
            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!singInResult.Succeeded) return BadRequest("Invalid password");
            await _signInManager.SignInAsync(user, request.RememberMe);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest parameters)
        {
            
            var user = new PersonDataModel();
            user.UserName = parameters.UserName;
            await _mediator.Send(new RegisterPersonCommand(user));

            var result = await _userManager.CreateAsync(user, parameters.Password);
            if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);
            return await Login(new LoginRequest
            {
                UserName = parameters.UserName,
                Password = parameters.Password
            });
        }   


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        public CurrentUser CurrentUserInfo()
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
