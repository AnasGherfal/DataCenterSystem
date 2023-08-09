using AutoMapper;
using Infrastructure;
using Infrastructure.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.CustomerManagement.DeleteCustomerById;

public sealed record DeleteCustomerByIdCommandHandler :IRequestHandler<DeleteCustomerByIdCommand,MessageResponse>
{

    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public DeleteCustomerByIdCommandHandler(DataCenterContext dbContext, IMapper mapper, IConfiguration config)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _config = config;
    }

    public async Task<MessageResponse> Handle(DeleteCustomerByIdCommand request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Customers.Include(p=>p.Files.Where(x=> x.IsActive != GeneralStatus.Deleted))
            .Where(p => p.Id == Guid.Parse(request.Id!) && p.Status == GeneralStatus.Active)
            .FirstOrDefaultAsync(cancellationToken:cancellationToken) ?? throw new NotFoundException("عفوًا لا وجود لعميل بهذا الرقم");
        data.Status = GeneralStatus.Deleted;
        foreach (var file in data.Files)
            file.IsActive = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! بنجاح " + data.Name + " : لقد تم حذف العميل",
        };
    }
}
