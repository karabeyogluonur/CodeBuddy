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

            if (context.MailTemplates.Count() == 0)
            {
                context.MailTemplates.AddRange(new List<EmailTemplate>
                {
                    new EmailTemplate{
                    Name = MailTemplateDefaults.UserWelcomeMessage,
                    EmailAccountId = 1,
                    Body = "Welcome %User.FirstName% %User.LastName%, this is a welcome message!",
                    Active = true,
                    Subject = "Welcome the codeBuddy",
                    },
                    new EmailTemplate{
                    Name = MailTemplateDefaults.UserEmailConfirmationMessage,
                    EmailAccountId = 1,
                    Body = "Welcome %User.FirstName% %User.LastName%, this is a welcome message!",
                    Active = true,
                    Subject = "Welcome the codeBuddy",
                    }
                });
            }

            context.SaveChanges();
        }
    }
}
