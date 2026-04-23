using CRMSarritel.Shared.Models.DTO;
using CRMSarritelApi.Models;
using CRMSarritelApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(UsuarioDto request)
    {
        var result = await _authService.LoginAsync(request);
        if (result == null)
            return Unauthorized(new { message = "Credenciales inválidas" });

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult<Usuario>> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        if (result == null)
            return BadRequest(new { message = "El usuario ya existe o datos inválidos" });

        return Ok(result);
    }
}