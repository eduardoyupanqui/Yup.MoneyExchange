using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yup.MoneyExchange.Application.Currencies.Commands;
using Yup.MoneyExchange.Application.Dtos;

namespace Yup.MoneyExchange.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public CurrencyController(ILogger<CurrencyController> logger, IMediator mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(GenericResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateCurrencyCommand request)
    {
        //Sacar identificador del usuario logeado
        request.RegistredBy = Guid.NewGuid();

        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
