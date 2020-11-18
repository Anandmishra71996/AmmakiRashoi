using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

       

        public int StatusCode { get; set; }
        public string Message { get; set; }
         private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400=>"You have made bad request",
                401=>" You are not authorized yet",
                404=>"Document you are looking for is not found",
                500=> "Internal Server Error !!!!!!",
                _=>null
            };
        }
    }
}