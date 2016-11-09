using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Models;

namespace Telling.Core.Validators
{
    public class SessionValidator : AbstractValidator<Session>
    {
        public SessionValidator()
        {
            RuleFor(a => a.SessionDate).NotEmpty();
            RuleFor(a => a.GameId).NotEmpty();
            RuleFor(a => a.Venue).NotEmpty();
        }
    }
}
