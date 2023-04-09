using CB.Application.Abstractions.Repositories;
using CB.Application.Abstractions.Repositories.UnitOfWork;
using CB.Application.Abstractions.Services.Mail;
using CB.Domain.Entities.Mail;
using Microsoft.EntityFrameworkCore;

namespace CB.Services.Mail
{
    public class MailTemplateService : IMailTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<MailTemplate> _MailTemplateRepository;

        public MailTemplateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _MailTemplateRepository = _unitOfWork.GetRepository<MailTemplate>();
        }

        public async Task<MailTemplate> GetMailTemplatesByNameAsync(string MailTemplateName)
        {
            return await _MailTemplateRepository.GetFirstOrDefaultAsync(predicate: MailTemplate => MailTemplate.Name == MailTemplateName, include: inc => inc.Include(emailAccount => emailAccount.EmailAccount));
        }
    }
}
