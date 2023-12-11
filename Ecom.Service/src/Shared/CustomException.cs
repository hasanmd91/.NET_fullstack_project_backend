namespace Ecom.Service.src.Shared
{
    public class CustomException : Exception
    {

        public int StatusCode { get; set; }


        public CustomException(int statusCode, string msg) : base(msg)
        {
            StatusCode = statusCode;

        }

        public static CustomException NotFoundException(string msg = "Not Found")
        {
            return new CustomException(404, msg);
        }

        public static CustomException BadRequestException(string msg = "Bad Request")
        {
            return new CustomException(400, msg);
        }

        public static CustomException UnauthorizedException(string msg = "Unauthorized")
        {
            return new CustomException(401, msg);
        }

        public static CustomException InternalServerErrorException(string msg = "Internal Server Error")
        {
            return new CustomException(500, msg);
        }
        public static CustomException ForbiddenException(string msg = "Forbidden")
        {
            return new CustomException(403, msg);
        }



    }
}