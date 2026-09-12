using Microsoft.AspNetCore.Mvc;

namespace APARTMENT_API.Helpers
{
    public static class ApiResponse
    {
        //200 Ok
        public static IActionResult Success(object data, string message = "Success")
        {
            return new OkObjectResult(new
            {
                    success = true,
                    statusCode = 200,
                    message = message,
                    data = data
            });

        }

        internal static object? Error(string v)
        {
            throw new NotImplementedException();
        }
    }
}
