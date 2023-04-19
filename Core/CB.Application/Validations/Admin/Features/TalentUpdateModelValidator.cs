using CB.Application.Models.Admin.Features;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Application.Validations.Admin.Features
{
    public class TalentUpdateModelValidator : AbstractValidator<TalentUpdateModel>
    {
        public TalentUpdateModelValidator()
        {
            RuleFor(t => t.Name)
               .NotEmpty().WithMessage("Yetenek adı boş geçilemez.")
               .NotNull().WithMessage("Yetenek adı boş geçilemez.");

        }
    }
}
