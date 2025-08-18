using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Seeds
{
	public static class CategorySeed
	{
		public static List<Category> Categories = [

			new Category
			{
				Id = 1,
				Name = "Action",
				Slug = "action",
				Status =Domain.Enums.Status.ACCEPTED
			},
			new Category
			{
				Id = 2,
				Name = "Comedy",
				Slug = "comedy",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Category
			{
				Id = 3,
				Name = "Drama",
				Slug = "drama",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Category
			{
				Id = 4,
				Name = "Horror",
				Slug = "horror",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Category
			{
				Id = 5,
				Name = "Sci-Fi",
				Slug = "sci-fi",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Category
			{
				Id = 6,
				Name = "Romance",
				Slug = "romance",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Category
			{
				Id = 7,
				Name = "Documentary",
				Slug = "documentary",
				Status = Domain.Enums.Status.REJECTED
			},
			new Category
			{
				Id = 8,
				Name = "Animation",
				Slug = "animation",
				Status = Domain.Enums.Status.REJECTED
			},
			new Category
			{
				Id = 9,
				Name = "Thriller",
				Slug = "thriller",
				Status = Domain.Enums.Status.PENDING
			},
			new Category
			{
				Id = 10,
				Name = "Fantasy",
				Slug = "fantasy",
				Status = Domain.Enums.Status.PENDING
			}
			];
	}
}
