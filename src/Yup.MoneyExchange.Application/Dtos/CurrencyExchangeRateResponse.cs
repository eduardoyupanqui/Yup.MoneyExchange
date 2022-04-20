using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yup.MoneyExchange.Application.Dtos
{
    public class CurrencyExchangeRateResponse
    {
        public string CurrencyFrom { get; }
        public string CurrencyTo { get; }
        public decimal ExchangeRate { get; }
        public string Text { get => $"El tipo de cambio de {CurrencyFrom} a {CurrencyTo} es {ExchangeRate}"; }
        public CurrencyExchangeRateResponse(string currencyFrom, string currencyTo, decimal exchangeRate)
        {
            CurrencyFrom = currencyFrom;
            CurrencyTo = currencyTo;
            ExchangeRate = exchangeRate;
        }
    }
}
