using CB.Application.Models.Admin.Features;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Application.Validations.Admin.Features
{
    public class TalentCreateModelValidator : AbstractValidator<TalentCreateModel>
    {
        public TalentCreateModelValidator()
        {
            RuleFor(t => t.Name)
               .NotEmpty().WithMessage("Yetenek adı boş geçilemez.")
               .NotNull().WithMessage("Yetenek adı boş geçilemez.");

            RuleFor(t => t.ImageFile)
                .NotEmpty().WithMessage("Yetenek resmi boş geçilemez.")
                .NotNull().WithMessage("Yetenek resmi boş geçilemez.");
        }
    }
}
