using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatDogLover_Repository.DAO
{
	public class Role
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RoleID { get; set; }
		public string? RoleName { get; set; }
		public List<User>? Users { get; set; }
	}
}

