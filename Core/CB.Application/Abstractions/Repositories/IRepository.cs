using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CB.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : class
    {

        T GetFirstOrDefault(Expression<Func<T, bool>> predicate = null,
                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                  bool disableTracking = true,
                                  bool ignoreQueryFilters = false);
        TResult GetFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,
                                           Expression<Func<T, bool>> predicate = null,
                                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                           bool disableTracking = true,
                                           bool ignoreQueryFilters = false);

        Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        IQueryable<T> FromSql(string sql, params object[] parameters);

        T Find(params object[] keyValues);

        ValueTask<T> FindAsync(params object[] keyValues);

        ValueTask<T> FindAsync(object[] keyValues, CancellationToken cancellationToken);


        IQueryable<T> GetAll();


        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null,
                                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                  bool disableTracking = true,
                                                  bool ignoreQueryFilters = false);


        IQueryable<TResult> GetAll<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);


        Task<IList<T>> GetAllAsync();

        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
                                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                  bool disableTracking = true,
                                                  bool ignoreQueryFilters = false);


        Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        int Count(Expression<Func<T, bool>> predicate = null);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);

        long LongCount(Expression<Func<T, bool>> predicate = null);

        Task<long> LongCountAsync(Expression<Func<T, bool>> predicate = null);

        decimal Average(Expression<Func<T, bool>> predicate = null, Expression<Func<T, decimal>> selector = null);

        Task<decimal> AverageAsync(Expression<Func<T, bool>> predicate = null, Expression<Func<T, decimal>> selector = null);

        decimal Sum(Expression<Func<T, bool>> predicate = null, Expression<Func<T, decimal>> selector = null);

        Task<decimal> SumAsync(Expression<Func<T, bool>> predicate = null, Expression<Func<T, decimal>> selector = null);

        bool Exists(Expression<Func<T, bool>> selector = null);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> selector = null);

        T Insert(T entity);

        void Insert(params T[] entities);

        void Insert(IEnumerable<T> entities);

        ValueTask<EntityEntry<T>> InsertAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task InsertAsync(params T[] entities);

        Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        void Update(T entity);

        void Update(params T[] entities);

        void Update(IEnumerable<T> entities);

        void Delete(object id);

        void Delete(T entity);

        void Delete(params T[] entities);

        void Delete(IEnumerable<T> entities);

    }
}
