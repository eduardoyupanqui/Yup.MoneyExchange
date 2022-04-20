using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yup.MoneyExchange.Domain.AggregatesModel.Base;

namespace Yup.MoneyExchange.Domain.AggregatesModel;

public class Currency : Entity, IAggregateRoot
{
    public Guid CurrencyGuid { get; private set; }
    public string Name { get; private set; }
    public string Abreviature { get; private set; }
    public bool EsActive { get; private set; } = true;

    protected Currency()
    {

    }
    public Currency(string name, string abreviature)
    {
        CurrencyGuid = Guid.NewGuid();
        Name = name;
        Abreviature = abreviature;
    }
}
