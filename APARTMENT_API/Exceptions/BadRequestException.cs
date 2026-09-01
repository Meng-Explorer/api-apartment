namespace APARTMENT_API.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<string> Errors { get; }

        public BadRequestException(string message) : base(message)
        {
            Errors = new List<string> { message };
        }

        public BadRequestException(List<string> errors) : base(string.Join("; ", errors))
        {
            Errors = errors ?? new List<string>();
        }
    }
}
