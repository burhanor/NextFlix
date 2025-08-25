using AutoMapper;
using MediatR;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Application.Bases;
using NextFlix.Application.Interfaces;

namespace NextFlix.Application.Features.Movie.Queries.GetMovieViews
{
	public class GetMovieViewsQueryHandler(IUow uow,IMapper mapper, IMovieHelper movieHelper) :BaseHandler<Domain.Entities.MovieView>(uow,mapper),IRequestHandler<GetMovieViewsQueryRequest, GetMovieViewsQueryResponse>
	{
		public async  Task<GetMovieViewsQueryResponse> Handle(GetMovieViewsQueryRequest request, CancellationToken cancellationToken)
		{
			int count = await movieHelper.GetMovieViews(request.MovieId,cancellationToken);
			return new GetMovieViewsQueryResponse
			{
				Count = count
			};
		}
	}
}
