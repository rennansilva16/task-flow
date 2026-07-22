using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Services;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Shared.Requests;

namespace TaskFlowAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
    {
        UserResponse userResponse = await _userService.CreateUserAsync(request);
        return StatusCode(201, userResponse);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        LoginResponse? loginResponse = await _userService.LoginAsync(request);
        
        if (loginResponse == null)
        {
            return Unauthorized();
        }

        return Ok(loginResponse);
    }
}