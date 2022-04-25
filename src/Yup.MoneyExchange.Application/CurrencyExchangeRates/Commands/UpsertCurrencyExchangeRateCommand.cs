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

namespace Yup.MoneyExchange.Application.CurrencyExchangeRates.Commands
{
    public class UpsertCurrencyExchangeRateCommand : IRequest<GenericResult>
    {
        public Guid CurrencyFromId { get; set; }
        public Guid CurrencyToId { get; set; }
        public decimal Exchange { get; set; }
        [JsonIgnore]
        public Guid RegistredBy { get; set; }
        public UpsertCurrencyExchangeRateCommand(Guid currencyFromId, Guid currencyToId, decimal exchange)
        {
            CurrencyFromId = currencyFromId;
            CurrencyToId = currencyToId;
            Exchange = exchange;
        }

        public class UpsertCurrencyExchangeRateCommandHandler : IRequestHandler<UpsertCurrencyExchangeRateCommand, GenericResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IBaseRepository<CurrencyExchangeRate> _currencyExchangeRateRepository;

            public UpsertCurrencyExchangeRateCommandHandler(IUnitOfWork unitOfWork, IBaseRepository<CurrencyExchangeRate> currencyExchangeRateRepository)
            {
                _unitOfWork = unitOfWork;
                _currencyExchangeRateRepository = currencyExchangeRateRepository;
            }

            public async Task<GenericResult> Handle(UpsertCurrencyExchangeRateCommand request, CancellationToken cancellationToken)
            {
                var result = new GenericResult();
                //Validaciones
                //TODO: Llevar a FluentValidation
                if (request.CurrencyFromId == Guid.Empty || request.CurrencyToId == Guid.Empty)
                {
                    result.AddError("Debe ingresar un identificador existente del Tipo de Cambio existente.");
                    return result;
                }

                if (request.Exchange < 0)
                {
                    result.AddError("El tipo de cambio no deber ser igual o menor a 0.");
                    return result;
                }

                var currencyExchangeRateToUpdate = _currencyExchangeRateRepository.FindBy(x => 
                    x.CurrencyFromId == request.CurrencyFromId && 
                    x.CurrencyToId == request.CurrencyToId && 
                    x.EsActive && !x.EsEliminado, false).FirstOrDefault();

                if (currencyExchangeRateToUpdate is null)
                {
                    var currencyExchangeRateToAdd = new CurrencyExchangeRate(request.CurrencyFromId, request.CurrencyToId, request.Exchange);
                    currencyExchangeRateToAdd.SetCreateAudit(DateTime.Now, request.RegistredBy);
                    _currencyExchangeRateRepository.Add(currencyExchangeRateToAdd);
                }
                else
                {
                    currencyExchangeRateToUpdate!.SetNewExchange(request.Exchange);
                    currencyExchangeRateToUpdate!.SetUpdateAudit(DateTime.Now, request.RegistredBy);

                    _currencyExchangeRateRepository.Update(currencyExchangeRateToUpdate);
                }
                
                var saved = await _unitOfWork.SaveChangesAsync();
                return result;
            }
        }
    }
}
