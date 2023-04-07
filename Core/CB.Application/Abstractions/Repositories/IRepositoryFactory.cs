namespace CB.Application.Abstractions.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>(bool hasCustomRepository = false) where T : class;
    }
}
