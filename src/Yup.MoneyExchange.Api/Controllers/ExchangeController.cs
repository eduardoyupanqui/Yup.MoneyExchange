using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yup.MoneyExchange.Application.Dtos;
using Yup.MoneyExchange.Application.Exchanges.Commands;

namespace Yup.MoneyExchange.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ExchangeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ExchangeController> _logger;

    public ExchangeController(ILogger<ExchangeController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(GenericResult<ExchangeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(MakeExchangeCommand request)
    {
        //Sacar identificador del usuario logeado
        request.RegistredBy = Guid.NewGuid();

        //Leer de la session si es preferencial o no , o hacerlo interno en el handler llamar a la tabla usuarios
        request.Preferencial = false;

        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
