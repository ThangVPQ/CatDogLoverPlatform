using System;
using System.ComponentModel.DataAnnotations;

namespace CatDogLover_Repository.DAO
{
	public class NumberOfInteraction
	{
        [Key]
        public Guid NumberOfInteractionID { get; set; }
		public Guid UserID { get; set; }
		public User User { get; set; }
		public Guid NewsFeedID { get; set; }
		public NewsFeed NewsFeed { get; set; }
		public DateTime InsertDate { get; set; }
    }
}

