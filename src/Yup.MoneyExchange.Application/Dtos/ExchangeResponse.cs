using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yup.MoneyExchange.Application.Dtos
{
    public class ExchangeResponse
    {
        public decimal Amount { get; set; }
        public decimal AmountExchange { get; set; }
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal? ExchangeRatePreferencial { get; set; }
        public ExchangeResponse()
        {

        }
        public ExchangeResponse(decimal amount, decimal amountExchange, string currencyFrom, string currencyTo, decimal exchangeRate, decimal? exchangeRatePreferencial)
        {
            Amount = amount;
            AmountExchange = amountExchange;
            CurrencyFrom = currencyFrom;
            CurrencyTo = currencyTo;
            ExchangeRate = exchangeRate;
            ExchangeRatePreferencial = exchangeRatePreferencial;
        }
    }
}
