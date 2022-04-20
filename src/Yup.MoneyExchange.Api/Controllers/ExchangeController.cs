using MediatR;
using Microsoft.AspNetCore.Mvc;

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
}
