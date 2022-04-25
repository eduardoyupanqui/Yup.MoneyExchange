using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yup.MoneyExchange.Domain.AggregatesModel.Base;

namespace Yup.MoneyExchange.Domain.AggregatesModel;

public class CurrencyExchangeRate : Entity, IAggregateRoot
{
    public Guid CurrencyFromId { get; private set; }
    public Guid CurrencyToId { get; private set; }
    public decimal Exchange { get; private set; }
    public decimal? PreferencialExchange { get; private set; }

    public bool EsActive { get; private set; } = true;
    protected CurrencyExchangeRate()
    {
        EsActive = true;
    }
    public CurrencyExchangeRate(Guid currencyFromId, Guid currencyToId, decimal exchange, decimal? preferencialExchange)
    {
        CurrencyFromId = currencyFromId;
        CurrencyToId = currencyToId;
        Exchange = exchange;
        PreferencialExchange = preferencialExchange;
    }

    public void SetNewExchange(decimal exchange)
    {
        Exchange = exchange;
    }
}
