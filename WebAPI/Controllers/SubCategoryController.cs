using Domain.Entities.DTOs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubCategoryController : ControllerBase
{
    private readonly IServiceWrapper _services;

    public SubCategoryController(IServiceWrapper services)
    {
        _services = services;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var (res, err) = await _services.SubCategoryService.GetByIdAsync(id);
        if (err != null)
        {
            return BadRequest(err);
        }
        return Ok(res);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _services.SubCategoryService.GetAllAsync();
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SubCategoryDTO dto)
    {
        var (res, err) = await _services.SubCategoryService.CreateAsync(dto);
        if (err != null)
        {
            return BadRequest(err);
        }
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> Update(SubCategoryDTO dto)
    {
        var (res, err) = await _services.SubCategoryService.UpdateAsync(dto);
        if (err != null)
        {
            return BadRequest(err);
        }
        return Ok(res);
    }

}
