using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Seeds
{
	public static class TagSeed
	{
		public static List<Tag> Tags = [
			
			new Tag{
				Id = 1,
				Name = "Action",
				Slug = "action",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Tag{
				Id = 2,
				Name = "Comedy",
				Slug = "comedy",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Tag{
				Id = 3,
				Name = "Drama",
				Slug = "drama",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Tag{
				Id = 4,
				Name = "Horror",
				Slug = "horror",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Tag{
				Id = 5,
				Name = "Sci-Fi",
				Slug = "sci-fi",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Tag{
				Id = 6,
				Name = "Romance",
				Slug = "romance",
				Status = Domain.Enums.Status.ACCEPTED
			},
			new Tag{
				Id = 7,
				Name = "Documentary",
				Slug = "documentary",
				Status = Domain.Enums.Status.PENDING
			},
			new Tag{
				Id = 8,
				Name = "Thriller",
				Slug = "thriller",
				Status = Domain.Enums.Status.PENDING
			},
			new Tag{
				Id = 9,
				Name = "Fantasy",
				Slug = "fantasy",
				Status = Domain.Enums.Status.REJECTED
			},
			new Tag{
				Id = 10,
				Name = "Adventure",
				Slug = "adventure",
				Status = Domain.Enums.Status.REJECTED
			},
			];
	}
}
