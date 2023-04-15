using CB.Application.Utilities.Defaults;
using CB.Data.Contexts;
using CB.Domain.Entities.Mail;
using CB.Domain.Entities.Membership;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CB.Data.Seeds
{
    public class DbInitializer
    {
        public async static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<CBDbContext>();
            var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if(_roleManager== null)
                throw new ArgumentNullException(nameof(_roleManager));

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

            if(_roleManager.Roles.Count() == 0)
            {
                await _roleManager.CreateAsync(new()
                {
                    Name = "Administrator",
                });
                await _roleManager.CreateAsync(new()
                {
                    Name = "Member",
                });
            }

            if(_userManager.Users.Count() == 0)
            {
                await _userManager.CreateAsync(new()
                {
                    FirstName = "Onur",
                    LastName = "Karabeyoğlu",
                    Active = true,
                    Deleted = false,
                    Email = "onur@icloud.com",
                    Verified = true,
                    EmailConfirmed = true,
                    UserName = "onurkarabeyoglu",
                    Gender = "Erkek",
                    PhoneNumber = "+905455793137",
                }, "12345678");
                var adminUser = await _userManager.FindByEmailAsync("onur@icloud.com");
                await _userManager.AddToRoleAsync(adminUser, "Administrator");

            }
        }
    }
}
