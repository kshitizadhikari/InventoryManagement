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

    [HttpPost]
    public async Task<IActionResult> Post(CategoryDTO dto)
    {
        var (res, err)= await _services.CategoryService.CreateAsync(dto);
        if (err != null)
        {
            return BadRequest(err);
        }

        return Ok(res);
    }
    
  }
