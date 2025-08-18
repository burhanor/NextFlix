﻿using NextFlix.Domain.Concretes;

namespace NextFlix.Domain.Entities
{
	public class MovieCategory : EntityBase
	{
		public int MovieId { get; set; }
		public int CategoryId { get; set; }
		public byte DisplayOrder { get; set; }
		public virtual Movie Movie { get; set; }
		public virtual Category Category { get; set; }
	}
}
