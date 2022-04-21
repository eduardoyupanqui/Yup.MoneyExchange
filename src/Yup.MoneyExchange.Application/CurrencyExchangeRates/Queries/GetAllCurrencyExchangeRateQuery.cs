using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yup.MoneyExchange.Application.Dtos;
using Yup.MoneyExchange.Domain.AggregatesModel;
using Yup.MoneyExchange.Domain.Repositories;

namespace Yup.MoneyExchange.Application.CurrencyExchangeRates.Queries
{

    public class GetAllCurrencyExchangeRateQuery : IRequest<IEnumerable<CurrencyExchangeRateResponse>>
    {
        public GetAllCurrencyExchangeRateQuery()
        {
        }
        public class GetAllCurrencyExchangeRateQueryHandler : IRequestHandler<GetAllCurrencyExchangeRateQuery, IEnumerable<CurrencyExchangeRateResponse>>
        {
            private readonly IBaseRepository<Currency> _currencyRepository;
            private readonly IBaseRepository<CurrencyExchangeRate> _currencyExchangeRateRepository;
            public GetAllCurrencyExchangeRateQueryHandler(IBaseRepository<CurrencyExchangeRate> currencyExchangeRateRepository, IBaseRepository<Currency> currencyRepository)
            {
                _currencyExchangeRateRepository = currencyExchangeRateRepository;
                _currencyRepository = currencyRepository;
            }
            public Task<IEnumerable<CurrencyExchangeRateResponse>> Handle(GetAllCurrencyExchangeRateQuery request, CancellationToken cancellationToken)
            {
                var currencies = (from exchangeRateQuery in _currencyExchangeRateRepository.Query(true)
                                  join currencyFromQuery in _currencyRepository.Query(true) on exchangeRateQuery.CurrencyFromId equals currencyFromQuery.Id
                                  join currencyToQuery in _currencyRepository.Query(true) on exchangeRateQuery.CurrencyToId equals currencyToQuery.Id
                                  where exchangeRateQuery.EsActive && !exchangeRateQuery.EsEliminado
                                  select new CurrencyExchangeRateResponse(
                                          exchangeRateQuery.Id,
                                          currencyFromQuery.Name,
                                          currencyToQuery.Name,
                                          exchangeRateQuery.Exchange
                                      )
                                 );
                return Task.FromResult(currencies.AsEnumerable());
            }
        }
    }
}
