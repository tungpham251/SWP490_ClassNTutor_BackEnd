using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class Pagination
    {
        public int TotalItem { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPage { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }


        public Pagination(int totalItemInput, int pageInput = 0, int pageRange = 10, int pageSize = 10)
        {
            if (pageRange <= 0) pageRange = 10;
            if (pageSize <= 0) pageSize = 10;

            if (pageInput < 0)
            {
                pageInput = 0;
            }

            TotalItem = totalItemInput;
            PageSize = pageSize;

            int totalPage = (int)Math.Ceiling(TotalItem / (double)PageSize);

            if (pageInput > totalPage)
            {
                pageInput = totalPage;
            }

            int currentPage = pageInput > 0 ? pageInput : 1;
            int startPage = currentPage - pageRange / 2;
            int endPage = currentPage + ((int)Math.Ceiling((double)pageRange / 2) - 1);

            if (startPage <= 0)
            {
                endPage -= startPage - 1;
                startPage = 1;
            }

            if (endPage > totalPage)
            {
                endPage = totalPage;
                if (endPage > pageRange)
                {
                    startPage = endPage - (pageRange - 1);
                }
            }

            CurrentPage = currentPage;
            TotalPage = totalPage;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}
