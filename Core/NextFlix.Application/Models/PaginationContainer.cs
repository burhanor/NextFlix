﻿namespace NextFlix.Application.Models
{
	public class PaginationContainer<T>
	{
		public List<T> Items { get; set; } = new List<T>();
		public int TotalCount { get; set; }
		public int PageSize { get; set; }
		public int PageNumber { get; set; }
		public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);
		public bool HasPreviousPage => PageNumber > 1;
		public bool HasNextPage => PageNumber < TotalPages;
		public PaginationContainer()
		{

		}
		public PaginationContainer(int pageNumber , int pageSize, int totalCount)
		{
			TotalCount = totalCount;
			PageSize = pageSize;
			PageNumber = pageNumber;
		}
		public PaginationContainer(List<T> items, int totalCount, int pageSize, int pageNumber)
		{
			Items = items;
			TotalCount = totalCount;
			PageSize = pageSize;
			PageNumber = pageNumber;
		}
	}
}
