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

public class CreateCurrencyExchangeRateCommand : IRequest<bool>
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

    public class CreateCurrencyExchangeRateCommandHandler : IRequestHandler<CreateCurrencyExchangeRateCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<CurrencyExchangeRate> _currencyExchangeRateRepository;

        public CreateCurrencyExchangeRateCommandHandler(IUnitOfWork unitOfWork, IBaseRepository<CurrencyExchangeRate> currencyExchangeRateRepository)
        {
            _unitOfWork = unitOfWork;
            _currencyExchangeRateRepository = currencyExchangeRateRepository;
        }

        public async Task<bool> Handle(CreateCurrencyExchangeRateCommand request, CancellationToken cancellationToken)
        {
            //Validaciones

            var currencyExchangeRateToSave = new CurrencyExchangeRate(request.CurrencyFromId, request.CurrencyToId, request.Exchange);

            currencyExchangeRateToSave.SetCreateAudit(DateTime.Now, request.RegistredBy);
            var result = _currencyExchangeRateRepository.Add(currencyExchangeRateToSave);

            var xxx = await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
