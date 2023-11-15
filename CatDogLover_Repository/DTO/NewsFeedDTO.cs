﻿using CatDogLover_Repository.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLover_Repository.DTO
{
    public class NewsFeedDTO
    {
        public Guid NewsFeedID { get; set; }
        public string? UserName { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public long? InsertDated { get; set; }
        public long? UpdateDated { get; set; }
        public int Status { get; set; }
        public int CommentQuantity{ get; set; }
        public int LikeQuantity { get; set; }
        public List<ImageDTO> ListImages { get; set; }

        public List<UserInterested> UserInteresteds { get; set; } = new List<UserInterested>();
    }
}
