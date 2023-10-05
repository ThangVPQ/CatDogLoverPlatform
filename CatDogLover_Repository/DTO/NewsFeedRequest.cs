using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLover_Repository.DTO
{
    public class NewsFeedRequest
    {
        public Guid UserID { get; set; }
        public Guid TypeGoodsID { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<string>? ListImage { get; set; }
    }
}
