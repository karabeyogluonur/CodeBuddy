using CB.Application.Abstractions.Services.Mail;
using CB.Domain.Entities.Mail;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Net;

namespace CB.Services.Mail
{
    public class EmailSender : IEmailSender
    {
        private readonly IEmailAccountService _emailAccountService;
        public EmailSender(IEmailAccountService emailAccountService)
        {
            _emailAccountService = emailAccountService;
        }

        #region Smtp Client Build
        private async Task<SmtpClient> BuildSmtpAsync(EmailAccount emailAccount = null)
        {
            if (emailAccount is null)
               emailAccount = await _emailAccountService.GetDefaultEmailAccountAsync();


            SmtpClient smtpClient = new SmtpClient();

            try
            {
                await smtpClient.ConnectAsync(
                    emailAccount.Host,
                    emailAccount.Port,
                    emailAccount.EnableSsl);

                if (emailAccount.Username != null)
                    await smtpClient.AuthenticateAsync(new NetworkCredential(emailAccount.Username, emailAccount.Password));
                else
                    await smtpClient.AuthenticateAsync(CredentialCache.DefaultNetworkCredentials);

                return smtpClient;
            }
            catch (Exception ex)
            {
                smtpClient.Dispose();
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        public async Task SendEmailAsync(EmailAccount emailAccount, string subject, string body, string fromAddress, string fromName, string toAddress, string toName)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress(fromName, fromAddress));
            message.To.Add(new MailboxAddress(toName, toAddress));
            message.Subject = subject;
            message.Body = new Multipart("mixed") { new TextPart(TextFormat.Html) { Text = body } };

            using SmtpClient smtpClient = await BuildSmtpAsync(emailAccount);
            await smtpClient.SendAsync(message);
            await smtpClient.DisconnectAsync(true);
        }
    }
}
