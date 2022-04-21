using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yup.MoneyExchange.Application.Dtos
{
    public class CurrencyResponse
    {
        public Guid Currency { get; set; }
        public string Name { get; }
        public string Abreviature { get; }

        public CurrencyResponse(Guid currency, string name, string abreviature)
        {
            Currency = currency;
            Name = name;
            Abreviature = abreviature;
        }

    }
}
