using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yup.MoneyExchange.Application.Currencies.Commands;
using Yup.MoneyExchange.Application.Dtos;
using Yup.MoneyExchange.Domain.AggregatesModel;
using Yup.MoneyExchange.Infrastructure.Data;

namespace Yup.MoneyExchange.Api.Controllers
{
    [AllowAnonymous]
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

        /// <summary>
        /// Elimina la base de datos y la recrea, y tambien hace una carga inicial de registros.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            if (!_exchangeDbContext.Database.IsInMemory())
            {
                _exchangeDbContext.Database.EnsureDeleted();
                _exchangeDbContext.Database.Migrate();
            }

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
                var solesCurrency = _exchangeDbContext.Currency.FirstOrDefault(x => x.Abreviature == "SOL");
                var dolarCurrency = _exchangeDbContext.Currency.FirstOrDefault(x => x.Abreviature == "USA");
                var euroCurrency = _exchangeDbContext.Currency.FirstOrDefault(x => x.Abreviature == "EUR");

                _exchangeDbContext.CurrencyExchangeRate.AddRange(new List<CurrencyExchangeRate> {
                        new CurrencyExchangeRate(solesCurrency!.Id, dolarCurrency!.Id, 3.76m, 3.50m),
                        new CurrencyExchangeRate(solesCurrency!.Id, euroCurrency!.Id, 4.16m, 4.10m)
                    });
            }

            await _exchangeDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
