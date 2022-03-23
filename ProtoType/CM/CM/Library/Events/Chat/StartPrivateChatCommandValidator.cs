using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CM.Library.DataModels;
using CM.Library.Queries.Person;
using FluentValidation;
using MediatR;

namespace CM.Library.Events.Chat
{
	public class StartPrivateChatCommandValidator : AbstractValidator<StartPrivateChatCommand>
	{
		private readonly IMediator _mediator;

		public StartPrivateChatCommandValidator(IMediator mediator)
		{
			this._mediator = mediator;

			RuleFor(x => x.FirstPersonId).NotNull().NotEmpty().WithMessage("Fist Person Id should not be null or empty");
			RuleFor(x => x.SecondPersonId).NotNull().NotEmpty().WithMessage("secont Person Id should not be null or empty");
			RuleFor(x => x).MustAsync(async (startPrivateChatCommand, cancellation) => {
				return await oneOfThemBeAnAuthorizedUser(startPrivateChatCommand.FirstPersonId, startPrivateChatCommand.SecondPersonId, startPrivateChatCommand.ClaimsPrincipal);
			}).WithMessage("No one of the users authorized to start chat");
		}

		private async Task<bool> oneOfThemBeAnAuthorizedUser(string fistUserId , string secondUserId, ClaimsPrincipal claimsPrincipal)
        {
			PersonDataModel authorizedPerson = await _mediator.Send(new GetTheAuthorizedPersonQuery(claimsPrincipal));

			if (authorizedPerson.Id == fistUserId)
				return true;
			else if (authorizedPerson.Id == secondUserId)
				return true;
			else
				return false;

        }
	}
}

