using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Interfaces;
using NextFlix.Domain.Interfaces;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Bases
{
	public class DeleteHandler<T, TRequest>(IUow uow,  IMapper mapper, string successMessage, string failMessage, IRabbitMqService rabbitMqService, RabbitMqQueues queue) : BaseHandler<T>(uow,  mapper, rabbitMqService), IRequestHandler<TRequest, ResponseContainer<Unit>>
		where T : class, IEntityBase, new()
		where TRequest : class, IDeleteRequest, new()
	{

		private string FailMessage  = string.Empty;
		public async virtual Task<ResponseContainer<Unit>> Handle(TRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<Unit> response = new(ResponseStatus.Deleted,successMessage);
			bool beforeDeleteControl = await BeforeDeleteControl(request.Ids, cancellationToken: cancellationToken);
			if (!beforeDeleteControl)
			{
				response.Status = ResponseStatus.BadRequest;
				response.Message = FailMessage;
				return response;
			}
			try
			{
				if (request.Ids?.Count > 0)
				{
					writeRepository.Delete(request.Ids);
					response.Status = ResponseStatus.Deleted;
					response.Message = successMessage;
					await RabbitMqService.Publish(queue, RabbitMqRoutingKeys.Deleted, request.Ids, cancellationToken);
					await AfterDeleteSuccessAsync(request.Ids, cancellationToken);

				}

			}
			catch (Exception ex)
			{
				response.Status = ResponseStatus.Failed;
				response.Message = failMessage;
			}

			return response;
		}

		protected virtual async Task AfterDeleteSuccessAsync(List<int> deletedIds, CancellationToken cancellationToken)
		{
			await uow.SaveChangesAsync(cancellationToken);
		}

		protected virtual async Task<bool> BeforeDeleteControl(List<int> deletedIds,string failMessage="", CancellationToken cancellationToken=default)
		{
			FailMessage = failMessage;
			return await Task.FromResult(true);
		}
		


	}
}
