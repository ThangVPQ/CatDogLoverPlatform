using System;
using CatDogLover_Repository.DAO;
using Microsoft.AspNetCore.Http;

namespace CatDogLover_Repository.DTO
{
	public class NewsFeedForSaleRequest
	{
        public Guid UserID { get; set; }
        public Guid TypeGoodsID { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Address { get; set; }
        public Decimal? Price { get; set; }
        public string PhoneNumber { get; set; }
        public long? BirthDate { get; set; }
        public List<string>? ListImage { get; set; }
    }
}

