using Domain.Entities.DTOs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]

public class ProductController : ControllerBase
{

    private readonly IServiceWrapper _services;

    public ProductController(IServiceWrapper services)
    {
        _services = services;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var (res, err) = await _services.ProductService.GetByIdAsync(id);
        if (err != null)
        {
            return BadRequest(err);
        }
        return Ok(res);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _services.ProductService.GetAllAsync();
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductDTO dto)
    {
        var (res, err) = await _services.ProductService.CreateAsync(dto);
        if (err != null)
        {
            return BadRequest(err);
        }
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductDTO dto)
    {
        var (res, err) = await _services.ProductService.UpdateAsync(dto);
        if (err != null)
        {
            return BadRequest(err);
        }
        return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var err = await _services.ProductService.DeleteAsync(id);
        if (err != null)
        {
            return BadRequest(err);
        }
        return Ok();
    }

}
