using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using CM.Library.Events.Person;



namespace CM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IMediator _mediator { get; set; }
        private IConfiguration _configuration;

        public AuthenticationController(IMediator mediator, IConfiguration configuration)
        {
            this._mediator = mediator;
            this._configuration = configuration;
        }


        [HttpPost]
        [Route("LoginWithUsername")]
        public async Task<IActionResult> LoginWithUsername(string username, string password)
        {
            try
            {
                return  Ok( await _mediator.Send(new LoginPersonCommand(
                    username,password,
                    rememberMe: false,
                    issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                    audience: _configuration.GetValue<string>("Jwt:Audience")
                    )));
            }
            catch (ValidationException v)
            {
                return ValidationProblem(v.Message);
            }
            catch
            {
                return BadRequest("Unrecognized error ");

            }
        }

        [HttpPost]
        [Route("RegisterWithUsername")]
        public async Task<IActionResult> RegisterWithUsername(string username, string password,string confirmedPassword)
        {
            try
            {
                return Ok( await _mediator.Send(new RegisterPersonCommand(
                    username,
                    password,
                    confirmedPassword,
                    issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                    audience: _configuration.GetValue<string>("Jwt:Audience")
                    )));

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


        /// <summary>
        /// Step 1 - Gets the phonenumber from the user and send an OTP for the user 
        /// </summary>
        /// <param name="Phonenumber"></param>
        /// <returns>
        /// 1- No return
        /// 2- ValidationExeption if the user is not registered.
        /// </returns>
        [HttpPost]
        [Route("LoginWithPhonenumber")]
        public async Task<IActionResult> LoginWithPhonenumber(string Phonenumber)
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

        /// <summary>
        /// Step 2 - Confirm the phonenumber
        /// </summary>
        /// <param name="Phonenumber"></param>
        /// <param name="otp">the last otp of the user</param>
        /// <returns>
        /// 1- if authenticated - Reaturns the token
        /// 2- validation error if the OTP is not correct
        /// </returns>
        [HttpPost]
        [Route("ConfirmOTPLoginWithPhonenumber")]
        public async Task<IActionResult> ConfirmOTPLoginWithPhonenumber(string Phonenumber,string otp)
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


        [HttpPost]
        [Route("RegisterWithPhonenumber")]
        public async Task<IActionResult> RegisterWithPhonenumber(string Phonenumber)
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

