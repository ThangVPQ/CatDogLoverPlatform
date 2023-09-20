using System;
using System.ComponentModel.DataAnnotations;

namespace CatDogLover_Repository.DAO
{
	public class NewsFeed
	{
        [Key]
        public Guid NewsFeedID { get; set; }
		public Guid UserID { get; set; }
		public User User { get; set; }
		public Guid TypeGoodsID { get; set; }
		public TypeGoods TypeGoods { get; set; }
		public string PhoneNumber { get; set; }
        public string Title { get; set; }
		public string Content { get; set; }
		public string Address { get; set; }
		public Decimal Price { get; set; }
		public DateTime BirthDate { get; set; }
		public DateTime InsertDate { get; set; }
		public DateTime UpdateDate { get; set; }
		public Guid UpdateBy { get; set; }
		public int Status { get; set; }
		public List<Comment> Comments { get; set; }
		public List<NumberOfInteraction> NumberOfInteractions { get; set; }
		public List<Image> Images { get; set; }
    }
}

