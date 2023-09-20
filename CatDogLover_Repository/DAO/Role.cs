using System;
using System.ComponentModel.DataAnnotations;

namespace CatDogLover_Repository.DAO
{
	public class Role
	{
        [Key]
        public Guid RoleID { get; set; }
		public string RoleName { get; set; }
		public List<User> Users { get; set; }
	}
}

