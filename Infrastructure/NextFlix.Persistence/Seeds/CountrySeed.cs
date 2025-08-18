using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Seeds
{
	public static class CountrySeed
	{
		public static List<Country> Countries = [
			new Country
			{
				Id = 1,
				Name = "United States",
				Slug = "united-states",
				Flag ="",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Country
			{
				Id = 2,
				Name = "United Kingdom",
				Slug = "united-kingdom",
				Flag ="",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Country
			{
				Id = 3,
				Name = "Canada",
				Slug = "canada",
				Flag ="",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Country
			{
				Id = 4,
				Name = "Australia",
				Slug = "australia",
				Flag ="",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Country
			{
				Id = 5,
				Name = "Germany",
				Slug = "germany",
				Flag ="",
				Status = Domain.Enums.Status.ACCEPTED
			},
			];
	}
}
