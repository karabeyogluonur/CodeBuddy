using CB.Application.Abstractions.Repositories;
using CB.Application.Abstractions.Repositories.UnitOfWork;
using CB.Application.Abstractions.Services.Mail;
using CB.Domain.Entities.Mail;

namespace CB.Services.Mail
{
    public class EmailAccountService : IEmailAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<EmailAccount> _emailAccountRepository;

        public EmailAccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _emailAccountRepository = _unitOfWork.GetRepository<EmailAccount>();
        }

        public async Task<EmailAccount> GetDefaultEmailAccountAsync()
        {
            return await _emailAccountRepository.GetFirstOrDefaultAsync(predicate: emailAccount => emailAccount.Default);
        }

        public async Task<EmailAccount> GetEmailAccountByIdAsync(int emailAccountId)
        {
            return await _emailAccountRepository.GetFirstOrDefaultAsync(predicate: emailAccount => emailAccount.Id == emailAccountId);
        }
    }
}
