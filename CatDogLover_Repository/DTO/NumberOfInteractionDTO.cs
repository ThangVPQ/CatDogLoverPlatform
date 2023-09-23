using CatDogLover_Repository.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLover_Repository.DTO
{
    public class NumberOfInteractionDTO
    {
        public Guid NumberOfInteractionID { get; set; }
        public string UserName { get; set; }
        public long InsertDated { get; set; }
    }
}
