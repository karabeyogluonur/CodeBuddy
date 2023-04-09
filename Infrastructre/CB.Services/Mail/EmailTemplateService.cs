using CB.Application.Abstractions.Repositories;
using CB.Application.Abstractions.Repositories.UnitOfWork;
using CB.Application.Abstractions.Services.Mail;
using CB.Domain.Entities.Mail;
using Microsoft.EntityFrameworkCore;

namespace CB.Services.Mail
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<EmailTemplate> _emailTemplateRepository;
        public EmailTemplateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _emailTemplateRepository = _unitOfWork.GetRepository<EmailTemplate>();
        }

        public async Task<EmailTemplate> GetEmailTemplatesByNameAsync(string emailTemplateName)
        {
            return await _emailTemplateRepository.GetFirstOrDefaultAsync(predicate: emailTemplate => emailTemplate.Name == emailTemplateName, include: inc => inc.Include(emailAccount => emailAccount.EmailAccount));
        }
    }
}
