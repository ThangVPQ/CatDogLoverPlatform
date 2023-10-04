using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLover_Repository.DAO
{
    public class TypeNewsFeed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TypesNewFeedID { get; set; }
        public string? TypesNewFeedName { get; set; }
        public List<NewsFeed>? NewsFeeds { get; set;}
    }
}
