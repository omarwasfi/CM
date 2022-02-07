using System;
using System.Threading.Tasks;
using CM.Library.DataModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace CM.Library.Events.Token
{
	public class CreateTokenByUserNameCommandValidator : AbstractValidator<CreateTokenByUserNameCommand>
	{
		private readonly UserManager<PersonDataModel> _userManager;

		public CreateTokenByUserNameCommandValidator(UserManager<PersonDataModel> userManager)
		{
			_userManager = userManager;

			CascadeMode = CascadeMode.Stop;

			RuleFor(x => x.UserName).MustAsync(async (username, cancellation) => {
				return await beARegisteredUsername(username);
			}).WithMessage("This username is not exist");

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

        
    }
}

