using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yup.MoneyExchange.Api.Helpers;
using Yup.MoneyExchange.Application.Dtos;

namespace Yup.MoneyExchange.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    private readonly TokenGenerator _tokenGenerator;

    public LoginController(
        ILogger<LoginController> logger,
        IMediator mediator,
        TokenGenerator tokenGenerator)
    {
        _mediator = mediator;
        _logger = logger;
        _tokenGenerator = tokenGenerator;
    }
    
    /// <summary>
    /// Iniciar sesion para obtener el token. Ejm: User: admin Password: admin
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("SignIn")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignIn(LoginRequest request)
    {
        //TODO: Validar que el usuario este registrado en el sistema

        return Ok(new { Token = _tokenGenerator.GenerateJwtToken(request.User)});
    }
}
