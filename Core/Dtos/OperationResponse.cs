using System.Net;

namespace Core.Dtos;

public class OperationResponse : MessageResponse
{
    public HttpStatusCode StatusCode { get; set; }
}