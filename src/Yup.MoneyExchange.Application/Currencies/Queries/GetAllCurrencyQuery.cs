using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yup.MoneyExchange.Application.Dtos;
using Yup.MoneyExchange.Domain.AggregatesModel;
using Yup.MoneyExchange.Domain.Repositories;

namespace Yup.MoneyExchange.Application.Currencies.Queries
{
    public class GetAllCurrencyQuery : IRequest<IEnumerable<CurrencyResponse>>
    {
        public GetAllCurrencyQuery()
        {
        }
        public class GetAllCurrencyQueryHandler : IRequestHandler<GetAllCurrencyQuery, IEnumerable<CurrencyResponse>>
        {
            private readonly IBaseRepository<Currency> _currencyRepository;
            public GetAllCurrencyQueryHandler(IBaseRepository<Currency> currencyRepository)
            {
                _currencyRepository = currencyRepository;
            }
            public Task<IEnumerable<CurrencyResponse>> Handle(GetAllCurrencyQuery request, CancellationToken cancellationToken)
            {
                var currencies = (from currency in _currencyRepository.Query(true)
                                 where currency.EsActive && !currency.EsEliminado
                                 select new CurrencyResponse(
                                     currency.Name,
                                     currency.Abreviature)
                                 );
                return Task.FromResult(currencies.AsEnumerable());
            }
        }
    }
}
