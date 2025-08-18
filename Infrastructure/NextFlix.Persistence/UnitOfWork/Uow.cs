using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Persistence.Context;
using NextFlix.Persistence.Repositories;

namespace NextFlix.Persistence.UnitOfWork
{
	
	public class Uow(MovieDbContext dbContext) : IUow
	{
		private readonly MovieDbContext dbContext = dbContext;
		public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();
		IReadRepository<T> IUow.GetReadRepository<T>() => new ReadRepository<T>(dbContext);
		IWriteRepository<T> IUow.GetWriteRepository<T>() => new WriteRepository<T>(dbContext);
		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await dbContext.SaveChangesAsync(cancellationToken);
		public int SaveChanges() => dbContext.SaveChanges();


		#region Transaction
		public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) => await dbContext.Database.BeginTransactionAsync(cancellationToken);
		public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
		{
			await SaveChangesAsync(cancellationToken);
			await dbContext.Database.CommitTransactionAsync(cancellationToken);
		}
		public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) => await dbContext.Database.RollbackTransactionAsync(cancellationToken);

		


		#endregion



	}
}
