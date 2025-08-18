using NextFlix.Domain.Concretes;
using NextFlix.Domain.Enums;

namespace NextFlix.Domain.Entities
{
	public class Country:EntityBase
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }
		public string Flag { get; set; }
		public virtual ICollection<Cast> Casts { get; set; } = [];
		public virtual ICollection<MovieCountry> MovieCountries { get; set; } = [];
	}
}
