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
            RuleFor(a => a.SessionDate).NotEmpty().WithMessage("You haven't selected a date.");
            RuleFor(a => a.Game).NotEmpty().WithMessage("You haven't selected a game.");
            RuleFor(a => a.Venue).NotEmpty().WithMessage("You haven't specified the venue.");
        }
    }
}
