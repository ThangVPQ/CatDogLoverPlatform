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
        public string? TypeGoodsName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Address { get; set; }
        public Decimal Price { get; set; }
        public long? BirthDated { get; set; }
        public long? InsertDated { get; set; }
        public long? UpdateDated { get; set; }
        public int Status { get; set; }
        public List<CommentDTO> CommentDTOs { get; set; }
        public List<NumberOfInteractionDTO> NumberOfInteractionDTOs { get; set; }
        public List<ImageDTO> ImageDTOs { get; set; }
    }
}
