using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yup.MoneyExchange.Application.Currencies.Commands;
using Yup.MoneyExchange.Application.CurrencyExchangeRates.Commands;

namespace Yup.MoneyExchange.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyExchangeRateController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public CurrencyExchangeRateController(ILogger<CurrencyExchangeRateController> logger, IMediator mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateCurrencyExchangeRateCommand request)
    {
        //Sacar identificador del usuario logeado
        request.RegistredBy = Guid.NewGuid();

        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
