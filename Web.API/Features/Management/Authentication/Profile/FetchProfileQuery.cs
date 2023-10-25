using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Authentication.Profile;
public sealed record FetchProfileQuery : IRequest<ContentResponse<FetchProfileQueryResponse>>
{
}