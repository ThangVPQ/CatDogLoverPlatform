﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CatDogLover_Repository.DAO
{
	public class Image
	{
        [Key]
        public Guid ImageID { get; set; }
		public Guid NewsFeedID { get; set; }
		public NewsFeed NewsFeed { get; set; }
		public byte[] SourceImage { get; set; }
		public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
