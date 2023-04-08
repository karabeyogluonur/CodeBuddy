using CB.Application.Models.User.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Application.Validations.User.Authentication
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(lm => lm.Email)
               .NotEmpty().WithMessage("Email boş geçilemez.")
               .NotNull().WithMessage("Email boş geçilemez.")
               .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(lm => lm.Password)
                .NotEmpty().WithMessage("Şifre boş geçilemez.")
                .NotNull().WithMessage("Şifre boş geçilemez.");
        }
    }
}
