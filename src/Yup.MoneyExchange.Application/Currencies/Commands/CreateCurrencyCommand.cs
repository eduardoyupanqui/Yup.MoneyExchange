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

namespace Yup.MoneyExchange.Application.Currencies.Commands;

public class CreateCurrencyCommand : IRequest<bool>
{
    public string Name { get; set; }
    public string Abreviature { get; set; }
    [JsonIgnore]
    public Guid RegistredBy { get; set; }
    public CreateCurrencyCommand(string name, string abreviature, Guid registredBy)
    {
        Name = name;
        Abreviature = abreviature;
        RegistredBy = registredBy;
    }


}
