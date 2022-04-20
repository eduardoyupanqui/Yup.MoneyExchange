using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yup.MoneyExchange.Application.Dtos;
using Yup.MoneyExchange.Application.Exchanges.Commands;

namespace Yup.MoneyExchange.Api.Controllers;

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

        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
