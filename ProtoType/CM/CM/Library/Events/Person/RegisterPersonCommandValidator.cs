﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;


namespace CM.Library.Events.Person
{
    public class RegisterPersonCommandValidator : AbstractValidator<RegisterPersonCommand>
    {
        public RegisterPersonCommandValidator()
        {
            RuleFor(x => x.PersonDataModel.UserName).NotNull().WithMessage("The Phone number can't be null");
            RuleFor(x => x.PersonDataModel.UserName).NotEmpty().WithMessage("The Phone number can't be empty");
            RuleFor(x => x.PersonDataModel.UserName).Length(5,20).WithMessage("user name can't be less than 10 chars");

        }
    }
}