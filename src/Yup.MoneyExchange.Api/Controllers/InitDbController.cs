using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yup.MoneyExchange.Application.Currencies.Commands;
using Yup.MoneyExchange.Application.Dtos;
using Yup.MoneyExchange.Domain.AggregatesModel;
using Yup.MoneyExchange.Infrastructure.Data;

namespace Yup.MoneyExchange.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InitDbController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ExchangeDbContext _exchangeDbContext;
        public InitDbController(ILogger<InitDbController> logger, ExchangeDbContext exchangeDbContext)
        {
            _logger = logger;
            _exchangeDbContext = exchangeDbContext;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _exchangeDbContext.Database.EnsureDeleted();
            _exchangeDbContext.Database.Migrate();

            if (!_exchangeDbContext.Currency.Any())
            {
                _exchangeDbContext.Currency.AddRange(new List<Currency> {
                        new Currency("Soles", "SOL"),
                        new Currency("Dolares", "USA"),
                        new Currency("Euros", "EUR")
                    });

                await _exchangeDbContext.SaveChangesAsync();
            }

            if (!_exchangeDbContext.CurrencyExchangeRate.Any())
            {
                _exchangeDbContext.CurrencyExchangeRate.AddRange(new List<CurrencyExchangeRate> {
                        new CurrencyExchangeRate(1, 2, 3.76m),
                    });
            }

            await _exchangeDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
