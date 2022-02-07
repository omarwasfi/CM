using CM.Library.DataModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.Person
{
    public class LoginPersonCommandValidator : AbstractValidator<LoginPersonCommand>
    {
        private readonly UserManager<PersonDataModel> _userManager;
        private readonly SignInManager<PersonDataModel> _signInManager;

        public LoginPersonCommandValidator(UserManager<PersonDataModel> userManager, SignInManager<PersonDataModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Usernamme).MustAsync(async (username, cancellation) => {
                return await beARegisteredUsername(username);
            }).WithMessage("This username is not exist");

            RuleFor(x => x).MustAsync(async (loginPersonCommand, cancellation) => {
                return await beACorrectPassword(loginPersonCommand.Usernamme,loginPersonCommand.Password);
            }).WithMessage("The password is not correct");

        }

        private async Task<bool> beARegisteredUsername(string username)
        {
            PersonDataModel user = await _userManager.FindByNameAsync(username);
            
            if (user != null)
            {
                return true;
            }
            else
                return false;
        }

        private async Task<bool> beACorrectPassword(string username, string password)
        {
            PersonDataModel user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return false;

            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!singInResult.Succeeded)
                return false;
            else
                return true;
        }

    }
}
