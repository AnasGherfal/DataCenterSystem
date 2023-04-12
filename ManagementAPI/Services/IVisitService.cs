﻿using ManagementAPI.Dtos.Visit;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IVisitService
{
    public Task<OperationResponse> Create(CreateVisitRequestDto request);
    public Task<FetchVisitResponseDto> GetAll(FetchVisitRequestDto request);
    public Task<OperationResponse> Lock(int id);
    public Task<OperationResponse> Unlock(int id);
    public Task<OperationResponse> Update(int id, UpdateVisitRequestDto request);
    public Task<OperationResponse> Delete(int id);
    public Task<OperationResponse> Paid(int id, int InvoiceId);
}
