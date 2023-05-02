using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Shared.Dtos;

public class OperationResponse : MessageResponse
{
    public HttpStatusCode StatusCode { get; set; }
}