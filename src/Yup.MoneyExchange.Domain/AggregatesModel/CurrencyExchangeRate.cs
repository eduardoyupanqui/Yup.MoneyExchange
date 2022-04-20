using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yup.MoneyExchange.Domain.AggregatesModel.Base;

namespace Yup.MoneyExchange.Domain.AggregatesModel;

public class CurrencyExchangeRate : Entity, IAggregateRoot
{
    public Guid CurrencyExchangeRateGuid { get; }
    public int CurrencyFromId { get; }
    public int CurrencyToId { get; }
    public decimal Exchange { get; }

    public bool EsActive { get; } = true;
    protected CurrencyExchangeRate()
    {
        EsActive = true;
    }
    public CurrencyExchangeRate(int currencyFromId, int currencyToId, decimal exchange)
    {
        CurrencyExchangeRateGuid = Guid.NewGuid();
        CurrencyFromId = currencyFromId;
        CurrencyToId = currencyToId;
        Exchange = exchange;
    }
}
