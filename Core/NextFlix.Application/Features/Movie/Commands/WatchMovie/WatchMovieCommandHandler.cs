using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Movie.Commands.WatchMovie
{
	public class WatchMovieCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.MovieView>(uow, httpContextAccessor, mapper), IRequestHandler<WatchMovieCommandRequest, ResponseContainer<WatchMovieCommandResponse>>
	{
		public async Task<ResponseContainer<WatchMovieCommandResponse>> Handle(WatchMovieCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<WatchMovieCommandResponse> response = new();
			bool movieIsExist = await uow.GetReadRepository<Domain.Entities.Movie>().ExistAsync(m => m.Id == request.MovieId, cancellationToken);
			if (!movieIsExist)
			{
				response.ValidationErrors =
				[
					new ValidationError
					{
						ErrorMessage = MovieMessages.NOT_FOUND,
						PropertyName = nameof(request.MovieId)
					}
				];
				return response;
			}
			writeRepository.Delete(m => m.MovieId == request.MovieId && m.IpAddress == ipAddress && m.ViewDate.Date == DateTime.Today);
			Domain.Entities.MovieView movieView = new()
			{
				MovieId = request.MovieId,
				IpAddress = ipAddress,
				ViewDate = DateTime.UtcNow,

			};
			await writeRepository.AddAsync(movieView, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (movieView.Id > 0)
			{
				response.Status = ResponseStatus.Success;
				response.Message = MovieMessages.VIEW_SUCCESS;
				response.Data = new WatchMovieCommandResponse();
				await rabbitMqService.Publish(RabbitMqQueues.MovieViews, RabbitMqRoutingKeys.Updated, request.MovieId, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.Failed;
				response.Message = MovieMessages.VIEW_FAILED;
			}
			return response;
		}
	}
}
