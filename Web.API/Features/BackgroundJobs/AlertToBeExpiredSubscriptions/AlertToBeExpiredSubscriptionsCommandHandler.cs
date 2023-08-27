using Infrastructure;
using MediatR;
using Web.API.Services.MailService;

namespace Web.API.Features.BackgroundJobs.AlertToBeExpiredSubscriptions;

public sealed record AlertToBeExpiredSubscriptionsCommandHandler : IRequestHandler<AlertToBeExpiredSubscriptionsCommand, bool>
{
    private readonly AppDbContext _dbContext;
    private readonly IMailService _mail;
    public AlertToBeExpiredSubscriptionsCommandHandler(IMailService mail, AppDbContext dbContext)
    {
        _mail = mail;
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(AlertToBeExpiredSubscriptionsCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(new TimeSpan(1), cancellationToken);
        return true;
    }
}