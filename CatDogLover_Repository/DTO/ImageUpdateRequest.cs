using CatDogLover_Repository.DAO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLover_Repository.DTO
{
    public class ImageUpdateRequest
    {
        public Guid? ImageID { get; set; }
        public IFormFile SourceImage { get; set; }
    }
}
