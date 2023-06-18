using AutoMapper;
using Azure.Core;
using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Service;
using ManagementAPI.Dtos.Subscriptions;
using ManagementAPI.Dtos.User;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Shared.Dtos;
using Shared.Exceptions;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ManagementAPI.Services;

public class UserService
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(DataCenterContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<MessageResponse> Create(CreateUserRequestDto request)
    {
        if (await _dbContext.Users.AnyAsync(p => p.FullName == request.FullName && p.Status != GeneralStatus.Deleted))
            throw new BadRequestException("! thear is simillar user");

        /* List<Hr> Hrs = new List<Hr>();
         Hrs.Add(new Hr() {Id = 1,HasApp = true,UserName = "admin",FullName = "ehmeda",EmpId = 1001 });
         Hrs.Add(new Hr() { Id = 2, HasApp = false, UserName = "admin1", FullName = "ashraf", EmpId = 1002 });
         var Userdata = Hrs.SingleOrDefault(a => a.Id == request.UserId);
         if (Userdata == null)
         {
            return new OperationResponse()
             {
                 StatusCode = HttpStatusCode.BadRequest,
                 Msg = "there is no username with this number in ltt portal system"
             };
         }*/
        var data = _mapper.Map<User>(request);
        var generatedPassword = GeneratePassword();
        data.Password = HashPassword(await generatedPassword);
        await _dbContext.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse { Msg = "ok user created"};
    }
    public async Task<FetchUsersResponseDto> GetAll(FetchUsersRequestDto request)
    {
        var query = _dbContext.Users
           .Where(p => p.Status != GeneralStatus.Deleted);
        var queryResult = await query.OrderBy(p => p.Id)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToListAsync() ?? throw new NotFoundException("there is no users in database");
        var result = _mapper.Map<List<UserResponseDto>>(queryResult);
        var totalCount = await query.CountAsync();
        var totalpages = (int)Math.Ceiling(totalCount / (decimal)request.PageSize);
        return new FetchUsersResponseDto()
        {
            Content = result,
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize,
            TotalPages = totalpages,
        };

    }
    public async Task<UserResponseDto> GetById(int id)
    {
        if (id < 0) throw new BadRequestException("! طلبك غير صالح يرجى إعادة المحاولة");
        var query = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("there is no user with this number");
        var data = _mapper.Map<UserResponseDto>(query);
        return data;
    }
    private readonly Random random = new();

    public Task<string> GeneratePassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var newPassword = Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray();
        
       return Task.FromResult(new string(newPassword));
    }
    private static string HashPassword(string password)
    {
        using SHA256 sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
    public async Task<MessageResponse> Reset(int id)
    {
        if (id < 0) throw new BadRequestException("! طلبك غير صالح يرجى إعادة المحاولة");
        var query = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("there is no user with this number");
        if (query.Status == GeneralStatus.LockedByUser) throw new BadRequestException("user is locked ");
        var generatedPassword = GeneratePassword();
        query.Password = HashPassword(await generatedPassword);
        await _dbContext.SaveChangesAsync();
        //todo:send email with newPassword
        return new MessageResponse()
        {
            Msg="ok reset password "
            
        };
    }
    public async Task<MessageResponse> Delete(int id)
    {
        if (id < 0) throw new BadRequestException("! incorrect user id ");
        var query = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("there is no user with this number");
        if (query.Status == GeneralStatus.LockedByUser) throw new BadRequestException("user is locked ");
        query.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "ok user is deleted "

        };

    }
    public async Task<MessageResponse> Edit(int id,UpdateUserRequestDto request)
    {
        if (id < 0) throw new BadRequestException("! incorrect user id ");
        var query = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("there is no user with this number");
        if (query.Status == GeneralStatus.LockedByUser) throw new BadRequestException("user is locked ");
        var data = _mapper.Map<User>(request);
        await _dbContext.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "user updated"
        };

    }
    public async Task<MessageResponse> Lock(int id)
    {
        if (id < 0) throw new BadRequestException("! incorrect  id ");
        var data = await _dbContext.Users.FirstOrDefaultAsync(b => b.Id == id && b.Status != GeneralStatus.Deleted)?? throw new NotFoundException("thear is no user with this number");

        
        if (data.Status == GeneralStatus.LockedByUser)
        {
            throw new BadRequestException("عفوًا المستخدم مقفل مسبقًا !");
        }
        data.Status = GeneralStatus.LockedByUser;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "  لقد تم قفل المستخدم: " + " بنجاح! ",
        };
    }
    public async Task<MessageResponse> Unlock(int id)
    {
        if (id < 0) throw new BadRequestException("! incorrect  id ");
        var data = await _dbContext.Users.FirstOrDefaultAsync(b => b.Id == id && b.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("thear is no user with this number");
        if (data.Status == GeneralStatus.Active)
        {
            throw new BadRequestException("عفوًا المستخدم غير مقفل مسبقًا !");
        }
        data.Status = GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "  لقد تم فك قفل المستخدم: " + " بنجاح! ",
        };
    }
}
