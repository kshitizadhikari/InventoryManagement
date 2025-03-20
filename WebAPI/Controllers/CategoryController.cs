using Domain.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    
    [HttpPost]
    public async IActionResult Post(CategoryDTO dto)
    {
        var res = await _
    }
}
