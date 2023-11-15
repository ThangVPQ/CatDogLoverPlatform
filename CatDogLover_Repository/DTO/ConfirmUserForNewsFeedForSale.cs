using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLover_Repository.DTO
{
    public class ConfirmUserForNewsFeedForSale
    {
        public Guid NewsFeedID { get; set; }
        public Guid UserID { get; set; }
    }
}
