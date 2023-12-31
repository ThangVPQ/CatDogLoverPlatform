﻿using CatDogLover_Repository.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLover_Repository.DTO
{
    public class UpdateNewFeedRequest
    {
        public Guid UserID { get; set; }
        public Guid NewsFeedID { get; set; }
        public Guid? TypeGoodsID { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? Status { get; set; }
        public List<ImageUpdateRequest> ImageUpdateRequests { get; set; }
    }
}
