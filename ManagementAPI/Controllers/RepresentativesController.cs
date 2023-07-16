﻿using ManagementAPI.Dtos.Representative;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace ManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RepresentativesController : ControllerBase
{
        private readonly IRepresentativeService _service;
    public RepresentativesController(IRepresentativeService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRepresentativeRequestDto request)
        => Ok(await _service.Create(request));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] FetchRepresentativeRequestDto filter)
        => Ok(await _service.GetAll(filter));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
       => Ok(await _service.GetById(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
        => Ok( await _service.Delete(id));

    [HttpPut("{id}/lock")]
    public async Task<IActionResult> Lock(Guid id)
        =>Ok( await _service.Lock(id));

    [HttpPut("{id}/Unlock")]
    public async Task<IActionResult> Unlock(Guid id) 
        =>  Ok(await _service.Unlock(id));
       
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRepresentativeRequestDto request)
    => Ok(await _service.Update(id, request));

}
