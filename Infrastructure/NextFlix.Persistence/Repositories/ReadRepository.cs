using Microsoft.EntityFrameworkCore;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Domain.Interfaces;
using System.Linq.Expressions;

namespace NextFlix.Persistence.Repositories
{
	public class ReadRepository<T>(DbContext dbContext) : IReadRepository<T> where T : class, IEntityBase, new()
	{
		private readonly DbContext dbContext = dbContext;
		private DbSet<T> Table { get => dbContext.Set<T>(); }

		public async Task<T?> GetAsync(int id, bool enableTracking = false, CancellationToken cancellationToken = default)
		{
			return !enableTracking
				? await Table.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
				: await Table.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		}
		public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool enableTracking = false, CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = Table;
			if (!enableTracking)
				query = query.AsNoTrackingWithIdentityResolution();
			return await query.Where(predicate).FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<TReturnType?> GetAsync<TReturnType>( Expression<Func<T, bool>> predicate, Expression<Func<T, TReturnType>> select, bool enableTracking = false, CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = Table;
			if (!enableTracking)
				query = query.AsNoTrackingWithIdentityResolution();
			return await query.Where(predicate).Select(select).FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<IList<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, bool enableTracking = false, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? currentPage = null, int? pageSize = null, CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = Table;
			if (!enableTracking)
				query = query.AsNoTrackingWithIdentityResolution();
			if (predicate != null)
				query = query.Where(predicate);
			if (orderBy != null)
				query = orderBy(query);
			if (currentPage.HasValue && pageSize.HasValue)
				query = query.Skip((currentPage.Value - 1) * pageSize.Value).Take(pageSize.Value);
			return await query.ToListAsync(cancellationToken);
		}

		public async Task<IList<TReturnType>> GetListAsync<TReturnType>(Expression<Func<T, bool>> predicate , Expression<Func<T, TReturnType>> select,  bool enableTracking = false, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? currentPage = null, int? pageSize = null, CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = Table;
			if (!enableTracking)
				query = query.AsNoTracking();

			query = query.Where(predicate);
			if (orderBy != null)
				query = orderBy(query);
			if (currentPage.HasValue && pageSize.HasValue)
				query = query.Skip((currentPage.Value - 1) * pageSize.Value).Take(pageSize.Value);
			return await query.Select(select).ToListAsync(cancellationToken);
		}

		public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default) => predicate == null ? await Table.CountAsync(cancellationToken) : await Table.CountAsync(predicate, cancellationToken);
		public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) => await Table.AnyAsync(predicate, cancellationToken);
		public async Task<bool> UniqueAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) => !await ExistAsync(predicate, cancellationToken);
		public IQueryable<T> Query() => Table;

		public async Task<IList<T>> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken = default)
		{
			return await query.ToListAsync(cancellationToken);
		}


		public async Task<IList<TReturnType>> ToListAsync<TReturnType>(IQueryable<T> query, Expression<Func<T, TReturnType>> select, CancellationToken cancellationToken = default)
		{
			return await query.Select(select).ToListAsync(cancellationToken);
		}


		public async Task<int> CountAsync(IQueryable<T> query, CancellationToken cancellationToken = default)
		{
			return await query.CountAsync();
		}

	}
}
