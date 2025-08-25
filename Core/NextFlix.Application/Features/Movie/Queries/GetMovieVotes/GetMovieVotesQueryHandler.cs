using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Movie.Queries.GetMovieVotes
{
	

	public class GetMovieVotesQueryHandler(IUow uow, IMapper mapper, IMovieHelper movieHelper) : BaseHandler<Domain.Entities.MovieLike>(uow, mapper), IRequestHandler<GetMovieVotesQueryRequest, List<GetMovieVotesQueryResponse>>
	{
		public async Task<List<GetMovieVotesQueryResponse>> Handle(GetMovieVotesQueryRequest request, CancellationToken cancellationToken)
		{
			var votes = await movieHelper.GetMovieVotes(request.MovieId, cancellationToken);
			if (votes is null)
				return [];

			return mapper.Map<List<GetMovieVotesQueryResponse>>(votes);
		}
	}
}
