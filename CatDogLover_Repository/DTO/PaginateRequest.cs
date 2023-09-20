using System;
using System.ComponentModel;

namespace CatDogLover_Repository.DTO
{
	public class PaginateRequest
	{
        [DefaultValue(1)]
        public int CurrentPage { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}

