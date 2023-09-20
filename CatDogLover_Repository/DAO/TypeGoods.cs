using System;
using System.ComponentModel.DataAnnotations;

namespace CatDogLover_Repository.DAO
{
	public class TypeGoods
	{
        [Key]
        public Guid TypeGoodsID { get; set; }
		public string TypeGoodsName { get; set; }
		public List<NewsFeed> NewsFeeds { get; set; }
    }
}

