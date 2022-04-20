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
using Yup.MoneyExchange.Application.Dtos;

namespace Yup.MoneyExchange.Application.Exchanges.Commands;

public class MakeExchangeCommand : IRequest<GenericResult<ExchangeResponse>>
{
    public int CurrencyFromId { get; set; }
    public int CurrencyToId { get; set; }
    public decimal Amount { get; set; }
    [JsonIgnore]
    public Guid RegistredBy { get; set; }
    public MakeExchangeCommand(int currencyFromId, int currencyToId, decimal amount, Guid registredBy)
    {
        CurrencyFromId = currencyFromId;
        CurrencyToId = currencyToId;
        Amount = amount;
        RegistredBy = registredBy;
    }

    public class MakeExchangeCommandHandler : IRequestHandler<MakeExchangeCommand, GenericResult<ExchangeResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Currency> _currencyRepository;
        private readonly IBaseRepository<CurrencyExchangeRate> _currencyExchangeRateRepository;

        public MakeExchangeCommandHandler(IUnitOfWork unitOfWork, 
            IBaseRepository<CurrencyExchangeRate> currencyExchangeRateRepository, 
            IBaseRepository<Currency> currencyRepository)
        {
            _unitOfWork = unitOfWork;
            _currencyExchangeRateRepository = currencyExchangeRateRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task<GenericResult<ExchangeResponse>> Handle(MakeExchangeCommand request, CancellationToken cancellationToken)
        {
            var result = new GenericResult<ExchangeResponse>();
            //Validaciones
            //TODO: Llevar a FluentValidation
            if (request.CurrencyToId == 0 || request.CurrencyFromId == 0)
            {
                result.AddError("Debe ingresar Monedas existentes.");
                return result;
            }
            if (request.CurrencyToId == request.CurrencyFromId)
            {
                result.AddError("No se puede convertir al mismo tipo de moneda.");
                return result;
            }
            if (request.Amount <= 0)
            {
                result.AddError("Debe ingresar un monto valido.");
                return result;
            }

            var currentCurrencyExchange = (from exchangeRateQuery in _currencyExchangeRateRepository.Query(true)
                                               join currencyFromQuery in _currencyRepository.Query(true) on exchangeRateQuery.CurrencyFromId equals currencyFromQuery.Id
                                               join currencyToQuery in _currencyRepository.Query(true) on exchangeRateQuery.CurrencyToId equals currencyToQuery.Id
                                               where exchangeRateQuery.CurrencyFromId == request.CurrencyFromId && exchangeRateQuery.CurrencyToId == request.CurrencyToId && exchangeRateQuery.EsActive && !exchangeRateQuery.EsEliminado
                                           select new ExchangeResponse(
                                                   request.Amount,
                                                   0,
                                                   currencyFromQuery.Name,
                                                   currencyToQuery.Name,
                                                   exchangeRateQuery.Exchange
                                               )).FirstOrDefault();

            if (currentCurrencyExchange is null)
            {
                result.AddError("No existe un tipo de cambio para estas monedas.");
                return result;
            }

            var exchangeRate = request.Amount * currentCurrencyExchange.ExchangeRate;

            currentCurrencyExchange!.ExchangeRate = exchangeRate;
            result.DataObject = currentCurrencyExchange;

            return result;
        }
    }
}
