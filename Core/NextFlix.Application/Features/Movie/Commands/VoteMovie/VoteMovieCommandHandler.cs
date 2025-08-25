using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NextFlix.Application.Abstraction.Enums;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Constants;
using NextFlix.Shared.Response;

namespace NextFlix.Application.Features.Movie.Commands.VoteMovie
{
	public class VoteMovieCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.MovieLike>(uow, httpContextAccessor, mapper), IRequestHandler<VoteMovieCommandRequest, ResponseContainer<VoteMovieCommandResponse>>
	{
		public async Task<ResponseContainer<VoteMovieCommandResponse>> Handle(VoteMovieCommandRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<VoteMovieCommandResponse> response = new();
			bool movieIsExist = await uow.GetReadRepository<Domain.Entities.Movie>().ExistAsync(m => m.Id == request.MovieId, cancellationToken);
			if(!movieIsExist)
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
			writeRepository.Delete(m => m.MovieId == request.MovieId && m.IpAddress == ipAddress && m.VoteDate.Date == DateTime.Today);
			Domain.Entities.MovieLike movieLike = new()
			{
				MovieId = request.MovieId,
				IpAddress = ipAddress,
				VoteDate = DateTime.UtcNow,
				Vote=request.Vote
				
			};
			await writeRepository.AddAsync(movieLike, cancellationToken);
			await uow.SaveChangesAsync(cancellationToken);
			if (movieLike.Id > 0)
			{
				response.Status = ResponseStatus.Success;
				response.Message = MovieMessages.VOTE_SUCCESS;
				response.Data = new VoteMovieCommandResponse();
				await rabbitMqService.Publish(RabbitMqQueues.MovieLikes, RabbitMqRoutingKeys.Updated,request.MovieId, cancellationToken);
			}
			else
			{
				response.Status = ResponseStatus.Failed;
				response.Message = MovieMessages.VOTE_FAILED;
			}
			return response;
		}
	}
}
