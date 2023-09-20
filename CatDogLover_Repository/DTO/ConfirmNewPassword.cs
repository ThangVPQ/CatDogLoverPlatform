using System;
namespace CatDogLover_Repository.DTO
{
	public class ConfirmNewPassword
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string Otp { get; set; }
	}
}

