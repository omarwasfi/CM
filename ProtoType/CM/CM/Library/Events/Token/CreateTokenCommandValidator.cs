using System;
using FluentValidation;

namespace CM.Library.Events.Token
{
    public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
	{
		public CreateTokenCommandValidator()
		{
		}
	}
}

