using NextFlix.Application.Models;

namespace NextFlix.Application.Features.Country.Commands.DeleteCountry
{
	public class DeleteCountryCommandRequest : DeleteRequest
	{
		public DeleteCountryCommandRequest():base()
		{
			
		}
		public DeleteCountryCommandRequest(int id):base(id)
		{
		}
		public DeleteCountryCommandRequest(List<int> ids) : base(ids)
		{
		}
	}
}
