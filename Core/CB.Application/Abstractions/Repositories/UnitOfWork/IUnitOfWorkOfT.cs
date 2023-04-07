using Microsoft.EntityFrameworkCore;

namespace CB.Application.Abstractions.Repositories.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext DbContext { get; }
        Task<int> SaveChangesAsync(params IUnitOfWork[] unitOfWorks);
    }
}
