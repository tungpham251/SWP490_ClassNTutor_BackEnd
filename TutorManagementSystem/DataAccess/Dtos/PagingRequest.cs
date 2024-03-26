namespace DataAccess.Dtos
{
    public class PagingRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageRange { get; set; }
    }
}
