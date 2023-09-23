using CatDogLover_Repository.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLover_Repository.DTO
{
    public class CommentDTO
    {
        public Guid CommentID { get; set; }
        public string UserName { get; set; }
        public NewsFeed NewsFeed { get; set; }
        public string Content { get; set; }
        public long InsertDated { get; set; }
        public long? UpdateDated { get; set; }
        public int Status { get; set; }
    }
}
