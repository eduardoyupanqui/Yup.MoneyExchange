using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Yup.MoneyExchange.Domain.Repositories;
using Yup.MoneyExchange.Domain.AggregatesModel;

namespace Yup.MoneyExchange.Application.CurrencyExchangeRates.Commands;

internal class CreateCurrencyExchangeRateCommand : IRequest<bool>
{
    public int CurrencyFromId { get; set; }
    public int CurrencyToId { get; set; }
    public decimal Exchange { get; set; }
    [JsonIgnore]
    public Guid RegistredBy { get; set; }
    public CreateCurrencyExchangeRateCommand(int currencyFromId, int currencyToId, decimal exchange, Guid registredBy)
    {
        CurrencyFromId = currencyFromId;
        CurrencyToId = currencyToId;
        Exchange = exchange;
        RegistredBy = registredBy;
    }

}
