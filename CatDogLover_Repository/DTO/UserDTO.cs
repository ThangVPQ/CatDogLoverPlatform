using System;
using CatDogLover_Repository.DAO;

namespace CatDogLover_Repository.DTO
{
	public class UserDTO
	{
        public Guid UserID { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public long BirthDate { get; set; }
        public long InsertedDated { get; set; }
        public long UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
    }
}

