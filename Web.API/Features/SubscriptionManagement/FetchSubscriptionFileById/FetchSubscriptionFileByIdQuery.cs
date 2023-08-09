using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptionFileById;
public sealed record FetchSubscriptionFileByIdQuery(string? Id) 
    : IRequest<FileStreamResult>;
