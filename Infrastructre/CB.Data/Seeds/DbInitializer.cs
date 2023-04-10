using CB.Application.Utilities.Defaults;
using CB.Data.Contexts;
using CB.Domain.Entities.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CB.Data.Seeds
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<CBDbContext>();
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.Database.Migrate();

            if (context.EmailAccounts.Count() == 0)
            {
                context.EmailAccounts.Add(new()
                {
                    DisplayName = "Test",
                    Email = "4ec6bd4ef4-2d0a41+1@inbox.mailtrap.io",
                    Username = "84306207dd6d3b",
                    Password = "8001f742270959",
                    EnableSsl = false,
                    Port = 587,
                    Host = "sandbox.smtp.mailtrap.io",
                    Default = true,
                });
            }

            context.SaveChanges();

            if (context.EmailTemplates.Count() == 0)
            {
                context.EmailTemplates.AddRange(new List<EmailTemplate>
                {
                    new EmailTemplate{
                    Name = MailTemplateDefaults.UserWelcomeMessage,
                    EmailAccountId = 1,
                    Body = "Merhaba %User.FirstName% %User.LastName%, bu bir hoşgeldin mesajı.",
                    Active = true,
                    Subject = "codeBuddy'e hoşgeldin",
                    },
                    new EmailTemplate{
                    Name = MailTemplateDefaults.UserEmailConfirmationMessage,
                    EmailAccountId = 1,
                    Body = "Merhaba %User.FirstName% %User.LastName%, bu bir e-posta doğrulama mesajı <a href='https://localhost:7134/Authentication/Confirmation?token=%Email.ConfirmationToken%'> Click Me</a>",
                    Active = true,
                    Subject = "codeBuddy - Lütfen e-posta adresini doğrula",
                    },
                    new EmailTemplate{
                    Name = MailTemplateDefaults.UserPasswordRecoveryMessage,
                    EmailAccountId = 1,
                    Body = "Merhaba %User.FirstName% %User.LastName%, bu bir şifre sıfırlama mesajı <a href='https://localhost:7134/Authentication/PasswordRecovery?uid=%User.Id%&token=%Password.RecoveryToken%'> Click Me</a>",
                    Active = true,
                    Subject = "codeBuddy - Şifre sıfırlama talebi",
                    }
                });
            }

            context.SaveChanges();
        }
    }
}
