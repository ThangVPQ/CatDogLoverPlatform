using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatDogLover_Repository.DAO
{
	public class Comment
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CommentID { get; set; }
		public Guid UserID { get; set; }
		public User User { get; set; }
		public Guid NewsFeedID { get; set; }
		public NewsFeed NewsFeed { get; set; }
		public string? Content { get; set; }
		public DateTime? InsertDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public Guid? UpdateBy { get; set; }
		public int? Status { get; set; }
    }
}

