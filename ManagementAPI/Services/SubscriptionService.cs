using AutoMapper;
using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Service;
using ManagementAPI.Dtos.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System.Net;

namespace ManagementAPI.Services
{
    public class SubscriptionService
    {
        private readonly DataCenterContext _dbContext;
        private readonly IMapper _mapper;
        public SubscriptionService(DataCenterContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }

        public async Task<OperationResponse> CreateSubscription(CreateSubscriptionDto request)
        {
            if (request == null)
            return new OperationResponse() { StatusCode=HttpStatusCode.BadRequest,
                Msg="enter subscription data "
             };

            var newsubs = _mapper.Map<Subscription>(request);
            await _dbContext.Subscriptions.AddAsync(newsubs);
            await _dbContext.SaveChangesAsync();
            return new OperationResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Msg = "ok"
            };


        }

        public async Task<FetchSubscriptionResponseDto> GetAllSubscription(int pagenum , int pagesize)
        {
            var Subs_Query = await _dbContext.Subscriptions
                .Include(a=>a.Service)
                .Include(a=>a.Customer)
                .OrderBy(p => p.Id)
                .Skip(pagesize * (pagenum - 1))
                .Take(pagesize)
                .ToListAsync();

            var result = _mapper.Map<List<SubscriptionResponseDto>>(Subs_Query);
            var totalCount = (from Cst in Subs_Query select Cst).Count();
            var totalpages = (int)Math.Ceiling(totalCount / 25.00);

            return new FetchSubscriptionResponseDto() { Content =result, CurrentPage = pagenum, TotalPages = pagesize };








        }
    }
}
