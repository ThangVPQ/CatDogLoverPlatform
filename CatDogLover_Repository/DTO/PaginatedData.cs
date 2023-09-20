using System;
namespace CatDogLover_Repository.DTO
{
	public class PaginatedData<T>
    {
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}

