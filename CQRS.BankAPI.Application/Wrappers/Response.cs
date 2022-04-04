namespace CQRS.BankAPI.Application.Wrappers
{
    public class Response<T>
    {
        public bool Succeeeded { get; set; }
        public string? Message { get; set; }
        public List<string? > Errors { get; set; }
        public T? Data { get; set; }

        public Response()
        {

        }
        public Response(T data, string message = null)
        {
            Succeeeded = true;
            Message = message;
            Data = data;

        }
        public Response(string message)
        {
            Succeeeded = false;
            Message = message;
        }
    }
}
