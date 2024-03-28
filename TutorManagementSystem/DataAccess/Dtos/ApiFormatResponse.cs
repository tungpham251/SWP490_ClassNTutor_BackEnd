namespace DataAccess.Dtos
{
    public class ApiFormatResponse
    {
        public int Code { get; set; }
        public bool Success { get; set; }
        public object? Data { get; set; }

        public ApiFormatResponse(int code, bool success, object? data)
        {
            Code = code;
            Success = success;
            Data = data;
        }
    }
}
