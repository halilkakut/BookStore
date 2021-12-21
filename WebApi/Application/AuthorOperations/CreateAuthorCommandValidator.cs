using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {

            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Birthdate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(4);
        }
    }
}