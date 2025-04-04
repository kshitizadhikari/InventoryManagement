﻿using Domain.Entities.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterUserDTO dto)
    {
        var err = await _authService.RegisterAsync(dto);
        if (err != null)
        {
            return BadRequest(err);
        }

        return Ok();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUserDTO dto)
    {
        var (res, err) = await _authService.LoginAsync(dto);
        if (err != null)
        {
            return BadRequest(err);
        }

        return Ok(res);
    }

    [HttpPost("AssignRole")]
    public async Task<IActionResult> AssignRole(RoleAssignment item)
    {
        var err = await _authService.AssignRoleAsync(item);
        if (err != null)
        {
            return BadRequest(err);
        }

        return Ok();
    }
}