using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Sharing
{
    public class ProductParams
    {
        public string? Sort {  get; set; }
        public int? categoryId { get; set; }
        public int MaxPageSize { get; set; } = 6;
        public string? Search { get; set; }


        private int _PageSize= 3;
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int PageNumber { get; set; } = 1;
    }
}
