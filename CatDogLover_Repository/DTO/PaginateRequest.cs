﻿using System;
using System.ComponentModel;

namespace CatDogLover_Repository.DTO
{
	public class PaginateRequest
	{
        public string? Search { get; set; }

        [DefaultValue(1)]
        public int CurrentPage { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}

