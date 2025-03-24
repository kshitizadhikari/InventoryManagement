using Domain.Entities.DTOs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly IServiceWrapper _services;

    public CategoryController(IServiceWrapper categoryService)
    {
        _services = categoryService;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var (res, err) = await _services.CategoryService.GetByIdAsync(id);
        if (err != null)
        {
            return BadRequest(err);
        }
        return Ok(res);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _services.CategoryService.GetAllAsync();
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CategoryDTO dto)
    {
        var (res, err) = await _services.CategoryService.CreateAsync(dto);
        if (err != null)
        {
            return BadRequest(err);
        }
        return Ok(res);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update(CategoryDTO dto)
    {
        var (res, err) = await _services.CategoryService.UpdateAsync(dto);
        if (err != null)
        {
            return BadRequest(err);
        }
        return Ok(res);
    }

}
