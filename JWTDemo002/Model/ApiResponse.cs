using System.Net;

namespace JWTDemo002.Model
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotImplemented;
        public dynamic Data { get; set; } = null;
        public string Message { get; set; }
    }

    public class LoginRsp
    {
        public string BearerToken { get; set; }
        public required User User { get; set; }
    }
}
