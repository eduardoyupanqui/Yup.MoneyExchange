using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yup.MoneyExchange.Application.Dtos
{
    public class CurrencyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; }
        public string Abreviature { get; }

        public CurrencyResponse(Guid id, string name, string abreviature)
        {
            Id = id;
            Name = name;
            Abreviature = abreviature;
        }

    }
}
