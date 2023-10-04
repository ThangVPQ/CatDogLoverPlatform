using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatDogLover_Repository.DAO
{
	public class TypeGoods	
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TypeGoodsID { get; set; }
		public string? TypeGoodsName { get; set; }
		public List<NewsFeed>? NewsFeeds { get; set; }
    }
}

