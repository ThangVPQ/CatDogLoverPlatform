using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLover_Repository.DTO
{
    public class LikeRequest
    {
        public Guid UserID { get; set; }
        public Guid? NewsFeedID { get; set; }
    }
}
