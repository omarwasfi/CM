using CM.Library.Queries.ProblemType;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.Problem
{
    public class AddProblemCommandValidator : AbstractValidator<AddProblemCommand>
    {

        private readonly IMediator _mediator;

        public AddProblemCommandValidator(IMediator mediator)
        {
            this._mediator = mediator;

            RuleFor(x => x.ProblemTypeId).MustAsync(async (problemTypeId, cancellation) =>
            {
                return await beExist(problemTypeId);
            }).WithMessage("This problem type id not exist");
        }

        private async Task<bool> beExist(string id)
        {
            if (await _mediator.Send(new GetProblemTypeByIdQuery(id)) != null)
                return true;
            else
                return false;
        }


    }
}
