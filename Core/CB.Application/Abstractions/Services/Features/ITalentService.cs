using CB.Domain.Entities.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Application.Abstractions.Services.Features
{
    public interface ITalentService
    {
        Task<List<Talent>> GetAllAsync(bool showDeactived = false, bool showDeleted = false);
        Task<Talent> GetByIdAsync(int talentId);
        Task CreateAsync(Talent talent);
        void Delete(Talent talent);
        void Update(Talent talent);


    }
}
