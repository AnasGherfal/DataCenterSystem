using System.Net;

namespace Shared.Dtos;

public class OperationResponse : MessageResponse
{
    public HttpStatusCode StatusCode { get; set; }
}