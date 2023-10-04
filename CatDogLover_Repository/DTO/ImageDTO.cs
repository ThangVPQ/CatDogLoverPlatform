using CatDogLover_Repository.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLover_Repository.DTO
{
    public class ImageDTO
    {
        public Guid ImageID { get; set; }
        public byte[] SourceImage { get; set; }
    }
}
