using System.Security.Claims;
using Core.Constants;
using Core.Entities;
using Core.Events.Abstracts;
using Core.Events.Customer;
using Core.Exceptions;
using Core.Interfaces.Dtos;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.CustomerManagement.CreateCustomer;

public sealed record CreateCustomerCommandNotification : IRequest<bool>
{
    public CreateCustomerCommand Content { get; set; }
    public string Password { get; set; } = string.Empty;
}

public sealed record CreateCustomerCommandNotificationHandler : IRequestHandler<CreateCustomerCommandNotification, bool>
{
    private readonly IMailService _mailService;

    public CreateCustomerCommandNotificationHandler(IMailService mailService)
    {
        _mailService = mailService;
    }

    public async Task<bool> Handle(CreateCustomerCommandNotification request, CancellationToken cancellationToken)
    {
        await _mailService.SendWelcomeEmail(request.Content.Name!, request.Content.Email!, request.Password);
        return true;
    }
}