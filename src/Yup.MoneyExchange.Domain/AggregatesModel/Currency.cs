using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yup.MoneyExchange.Domain.AggregatesModel.Base;

namespace Yup.MoneyExchange.Domain.AggregatesModel;

public class Currency : Entity, IAggregateRoot
{
    public Guid CurrencyGuid { get; set; }
    public string Name { get; set; }
    public string Abreviature { get; set; }
    public bool EsActive { get; set; } = true;

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
