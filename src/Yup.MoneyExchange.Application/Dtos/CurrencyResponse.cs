using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yup.MoneyExchange.Application.Dtos
{
    public class CurrencyResponse
    {
        public string Name { get; }
        public string Abreviature { get; }

        public CurrencyResponse(string name, string abreviature)
        {
            Name = name;
            Abreviature = abreviature;
        }

    }
}
