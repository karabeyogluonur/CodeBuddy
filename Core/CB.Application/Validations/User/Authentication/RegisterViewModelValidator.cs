using CB.Application.Models.User.Authentication;
using CB.Application.Utilities.Defaults;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Application.Validations.User.Authentication
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("Ad boş geçilemez.")
                .NotNull().WithMessage("Ad boş geçilemez.");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Soyad boş geçilemez.")
                .NotNull().WithMessage("Soyad boş geçilemez.");

            RuleFor(c => c.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş geçilemez.")
                .NotNull().WithMessage("Kullanıcı adı boş geçilemez.");

            RuleFor(lm => lm.Email)
                .NotEmpty().WithMessage("Email boş geçilemez.")
               .NotNull().WithMessage("Email boş geçilemez.")
               .EmailAddress().WithMessage("Lütfen geçerli bir email adresi giriniz.");

            RuleFor(lm => lm.Password).NotEmpty().WithMessage("Şifre boş geçilemez.")
                .NotNull().WithMessage("Şifre boş geçilemez.")
                .MinimumLength(8).WithMessage("Şifre en az 8, en fazla 36 karakter olmalıdır.")
                .MaximumLength(36).WithMessage("Şifre en az 8, en fazla 36 karakter olmalıdır.");

            RuleFor(lm => lm.ConfirmPassword)
               .NotNull()
               .Equal(lm => lm.Password).WithMessage("Parolalar birbiri ile eşleşmiyor. Lütfen kontrol ediniz.");
        }
    }
}
