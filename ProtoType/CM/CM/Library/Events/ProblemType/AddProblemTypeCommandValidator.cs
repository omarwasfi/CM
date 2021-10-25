using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.ProblemType
{
    public class AddProblemTypeCommandValidator : AbstractValidator<AddProblemTypeCommand>
    {
        public AddProblemTypeCommandValidator()
        {
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("The name of the problem type has to be 100 or less");
        }
    }
}
