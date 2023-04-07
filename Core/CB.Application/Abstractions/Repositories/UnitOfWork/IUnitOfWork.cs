namespace CB.Application.Abstractions.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>(bool hasCustomRepository = false) where T : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        int ExecuteSqlCommand(string sql, params object[] parameters);
    }
}
