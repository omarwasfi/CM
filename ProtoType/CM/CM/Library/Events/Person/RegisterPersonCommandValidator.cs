using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CM.Library.DataModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace CM.Library.Events.Person
{
    public class RegisterPersonCommandValidator : AbstractValidator<RegisterPersonCommand>
    {
        private readonly UserManager<PersonDataModel> _userManager;

        public RegisterPersonCommandValidator(UserManager<PersonDataModel> userManager)
        {
            this._userManager = userManager;

            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.UserName).NotNull().WithMessage("The UserName can't be null");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("The UserName can't be empty");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("The minimumLength is 6");
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("The password and confirmed password is no the same !");
            RuleFor(x => x.UserName).MustAsync(async (username, cancellation) => {
                return await notBeARegisteredUsername(username);
            }).WithMessage("This username is exist");
        }

        private async Task<bool> notBeARegisteredUsername(string username)
        {
            PersonDataModel user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
