using CatDogLover_Repository.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLover_Repository.DTO
{
    public class UserUpdate
    {
        public Guid UserID { get; set; }
        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public long? BirthDate { get; set; }
        public int? Status { get; set; }
    }
}
