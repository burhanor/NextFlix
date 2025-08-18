using Microsoft.EntityFrameworkCore;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Domain.Interfaces;
using System.Linq.Expressions;

namespace NextFlix.Persistence.Repositories
{
	public class WriteRepository<T>(DbContext dbContext) : IWriteRepository<T> where T : class, IEntityBase, new()
	{
		private DbSet<T> Table { get => dbContext.Set<T>(); }
		public async Task AddAsync(T entity, CancellationToken cancellationToken) => await Table.AddAsync(entity, cancellationToken);
		public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken) => await Table.AddRangeAsync(entities, cancellationToken);

		public bool Update(T entity)
		{
			T? existingEntity = Table.Find(entity.Id);
			if (existingEntity != null)
			{
				dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
				return true;
			}
			return false;


		}

		public void UpdateRange(IEnumerable<T> entities) => Table.UpdateRange(entities);

		public void Delete(T entity) => Table.Remove(entity);
		public void DeleteRange(IEnumerable<T> entities) => Table.RemoveRange(entities);
		public void Delete(Expression<Func<T, bool>> predicate) => Table.RemoveRange(Table.Where(predicate));

		public void Delete(List<int> ids) => Table.RemoveRange(Table.Where(x => ids.Contains(x.Id)));
	}
}
