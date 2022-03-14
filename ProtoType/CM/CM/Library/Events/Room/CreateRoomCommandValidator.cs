using System;
using FluentValidation;

namespace CM.Library.Events.Room
{
	public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
	{
		public CreateRoomCommandValidator()
		{
			CascadeMode = CascadeMode.Stop;

			RuleFor(x => x.People).NotNull().NotEmpty().WithMessage("The peaple list in the room can't be empty or null");
			RuleFor(x => x.People.Count).GreaterThanOrEqualTo(2).WithMessage("The minimum room is between two people");
		}
	}
}

