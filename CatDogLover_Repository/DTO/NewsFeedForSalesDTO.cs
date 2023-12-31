﻿using CatDogLover_Repository.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLover_Repository.DTO
{
    public class NewsFeedForSalesDTO
    {
        public Guid NewsFeedID { get; set; }
        public string? UserName { get; set; }
        public string? TypeGoodsName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Address { get; set; }
        public int? Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal Price { get; set; }
        public Guid? InterestedUserID { get; set; }
        public string? InterestedUserName { get; set; }
        public long? InsertDated { get; set; }
        public long? UpdateDated { get; set; }
        public int Status { get; set; }
        public int CommentQuantity { get; set; }
        public int LikeQuantity { get; set; }
        public List<UserInterested> UserInteresteds { get; set; } = new List<UserInterested>();

        public List<ImageDTO> ListImages { get; set; }
    }
}
