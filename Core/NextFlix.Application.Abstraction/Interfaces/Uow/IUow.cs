using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextFlix.Application.Abstraction.Interfaces.Uow
{
	public interface IUow
	{
		IReadRepository<T> GetReadRepository<T>() where T : class, IEntityBase, new();
		IWriteRepository<T> GetWriteRepository<T>() where T : class, IEntityBase, new();
		Task BeginTransactionAsync(CancellationToken cancellationToken = default);
		Task CommitTransactionAsync(CancellationToken cancellationToken = default);
		Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
		int SaveChanges();
	}
}
