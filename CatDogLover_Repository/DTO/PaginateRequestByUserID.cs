using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLover_Repository.DTO
{
    public class PaginateRequestByUserID
    {
        public Guid UserID { get; set; }
        public string? Search { get; set; }

        [DefaultValue(1)]
        public int CurrentPage { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}
