using NextFlix.Domain.Interfaces;
using System.Linq.Expressions;

namespace NextFlix.Application.Abstraction.Interfaces.Repositories
{
	public interface IWriteRepository<T> where T : class, IEntityBase, new()
	{
		Task AddAsync(T entity, CancellationToken cancellationToken);
		Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
		bool Update(T entity);
		void UpdateRange(IEnumerable<T> entities);
		void Delete(T entity);
		void DeleteRange(IEnumerable<T> entities);
		void Delete(Expression<Func<T, bool>> predicate);
		void Delete(List<int> ids);
	}
}
