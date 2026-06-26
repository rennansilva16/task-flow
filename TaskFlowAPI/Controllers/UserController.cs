using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Services;

namespace TaskFlowAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
    {
        UserResponse userResponse = await _userService.CreateUserAsync(request);
        return StatusCode(201, userResponse);
    }
}