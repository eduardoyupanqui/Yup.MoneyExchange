﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yup.MoneyExchange.Application.CurrencyExchangeRates.Commands;
using Yup.MoneyExchange.Application.CurrencyExchangeRates.Queries;
using Yup.MoneyExchange.Application.Dtos;

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

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<CurrencyExchangeRateResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCurrencyExchangeRateQuery());
        return Ok(result);
    }

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
}
