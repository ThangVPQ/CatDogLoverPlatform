using System;
using CatDogLover_Repository.DAO;

namespace CatDogLover_Repository.DTO
{
	public class UserRegisterRequest
	{
        public string Email { get; set; }
        public string PasswordInit { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime BirthDay { get; set; }
    }
}

