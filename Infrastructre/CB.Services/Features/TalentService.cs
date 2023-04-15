using CB.Application.Abstractions.Repositories;
using CB.Application.Abstractions.Repositories.UnitOfWork;
using CB.Application.Abstractions.Services.Features;
using CB.Domain.Entities.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Services.Features
{
    public class TalentService : ITalentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Talent> _talentRepository;
        public TalentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _talentRepository = _unitOfWork.GetRepository<Talent>();
        }
        public async Task CreateAsync(Talent talent)
        {
            if(talent == null)
                throw new ArgumentNullException(nameof(talent));

            talent.Deleted= false;
            talent.CreatedDate = DateTime.Now;
            talent.UpdatedDate = DateTime.Now;
            await _talentRepository.InsertAsync(talent);
            await _unitOfWork.SaveChangesAsync();
        }

        public void Delete(Talent talent)
        {
            talent.Deleted = true;
            talent.Active = false;
            talent.Name = talent.Name + "(Pasif)";
            Update(talent);
        }

        public async Task<List<Talent>> GetAllAsync(bool showDeactived = false, bool showDeleted = false)
        {
            IEnumerable<Talent> talents = await _talentRepository.GetAllAsync();
            if (!showDeactived)
                talents = talents.Where(talent => talent.Active == true);
            if (!showDeleted)
                talents = talents.Where(talent => talent.Deleted == false);

            return talents.ToList();
        }

        public Task<Talent> GetByIdAsync(int talentId)
        {
            return _talentRepository.GetFirstOrDefaultAsync(predicate: talent => talent.Id == talentId);
        }

        public void Update(Talent talent)
        {
            talent.UpdatedDate = DateTime.Now;
            _talentRepository.Update(talent);
            _unitOfWork.SaveChangesAsync();
        }
    }
}
