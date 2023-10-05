using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatDogLover_Repository.DAO
{
	public class Image
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ImageID { get; set; }
		public Guid NewsFeedID { get; set; }
		public NewsFeed NewsFeed { get; set; }
		public string? UrlImage{ get; set; }
		public DateTime? InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}

