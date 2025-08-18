using NextFlix.Domain.Interfaces;
using System.Linq.Expressions;

namespace NextFlix.Application.Abstraction.Interfaces.Repositories
{
	public interface IReadRepository<T> where T : class,IEntityBase,new()
	{
		Task<T?> GetAsync(int id, bool enableTracking = false, CancellationToken cancellationToken = default);
		Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool enableTracking = false, CancellationToken cancellationToken = default);
		Task<TReturnType?> GetAsync<TReturnType>(Expression<Func<T, bool>> predicate, Expression<Func<T, TReturnType>> select, bool enableTracking = false, CancellationToken cancellationToken = default);
		Task<IList<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, bool enableTracking = false, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? currentPage = null, int? pageSize = null, CancellationToken cancellationToken = default);
		Task<IList<TReturnType>> GetListAsync<TReturnType>(Expression<Func<T, bool>> predicate, Expression<Func<T, TReturnType>> select, bool enableTracking = false, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? currentPage = null, int? pageSize = null, CancellationToken cancellationToken = default);
		Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
		Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
		Task<bool> UniqueAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
		IQueryable<T> Query();
		Task<IList<T>> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken = default);
		Task<int> CountAsync(IQueryable<T> query, CancellationToken cancellationToken = default);

	}
}
