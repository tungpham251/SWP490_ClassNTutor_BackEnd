using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class ViewPaging<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public Pagination Pagination { get; set; }

        public ViewPaging(IEnumerable<T> items, Pagination pagination)
        {
            Items = items;
            Pagination = pagination;
        }
    }
}
