using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yup.MoneyExchange.Application.CurrencyExchangeRates.Commands;
using Yup.MoneyExchange.Application.CurrencyExchangeRates.Queries;
using Yup.MoneyExchange.Application.Dtos;

namespace Yup.MoneyExchange.Api.Controllers;

[Authorize]
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
    /// <summary>
    /// Obtener todos los tipos de cambios de las monedas registradas
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<CurrencyExchangeRateResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCurrencyExchangeRateQuery());
        return Ok(result);
    }


    /// <summary>
    /// Crear un nuevo tipo de cambio
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(GenericResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateCurrencyExchangeRateCommand request)
    {
        //Sacar identificador del usuario logeado
        request.RegistredBy = Guid.NewGuid();

        var result = await _mediator.Send(request);
        return Ok(result);
    }

    /// <summary>
    /// Actualizar el tipo de cambio
    /// </summary>
    /// <param name="request"></param>
    /// <param name="exchangeRateId"></param>
    /// <returns></returns>
    [HttpPut("{exchangeRateId:int}")]
    [ProducesResponseType(typeof(GenericResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateCurrencyExchangeRateCommand request, [FromRoute] int exchangeRateId)
    {
        //Sacar identificador del usuario logeado
        request.RegistredBy = Guid.NewGuid();

        request.CurrencyExchangeRateId = exchangeRateId;
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
