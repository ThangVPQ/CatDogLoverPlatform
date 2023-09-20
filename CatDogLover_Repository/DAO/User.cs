using System;
using System.ComponentModel.DataAnnotations;

namespace CatDogLover_Repository.DAO
{
	public class User
	{
		[Key]
		public Guid UserID { get; set; }
		public Guid RoleID { get; set; }
		public Role Role { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string FullName { get; set; }
		public string FirstName { get; set; }
		public string PhoneNumber { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public DateTime BirthDate { get; set; }
		public DateTime InsertDate { get; set; }
		public DateTime UpdateDate { get; set; }
		public DateTime UpdateBy { get; set; }
		public int Status { get; set; }
		public string Otp { get; set; }
		public DateTime LogOutDate { get; set; }
		public List<NewsFeed> NewsFeeds { get; set; }
		public List<Comment> Comments { get; set; }
		public List<NumberOfInteraction> NumberOfInteractions { get; set; }
	}
}

