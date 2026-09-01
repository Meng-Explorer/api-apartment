namespace APARTMENT_API.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; set; }
        public ValidationException(List<string> errors) : base("Validations Fail.")
        {
            Errors = errors;
        }
    }
}
