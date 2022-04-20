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
    public class UpdateCurrencyExchangeRateCommand : IRequest<GenericResult>
    {
        [JsonIgnore]
        public int CurrencyExchangeRateId { get; set; }
        public decimal Exchange { get; set; }
        [JsonIgnore]
        public Guid RegistredBy { get; set; }
        public UpdateCurrencyExchangeRateCommand(int currencyExchangeRateId, decimal exchange)
        {
            CurrencyExchangeRateId = currencyExchangeRateId;
            Exchange = exchange;
        }

        public class UpdateCurrencyExchangeRateCommandHandler : IRequestHandler<UpdateCurrencyExchangeRateCommand, GenericResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IBaseRepository<CurrencyExchangeRate> _currencyExchangeRateRepository;

            public UpdateCurrencyExchangeRateCommandHandler(IUnitOfWork unitOfWork, IBaseRepository<CurrencyExchangeRate> currencyExchangeRateRepository)
            {
                _unitOfWork = unitOfWork;
                _currencyExchangeRateRepository = currencyExchangeRateRepository;
            }

            public async Task<GenericResult> Handle(UpdateCurrencyExchangeRateCommand request, CancellationToken cancellationToken)
            {
                var result = new GenericResult();
                //Validaciones
                //TODO: Llevar a FluentValidation
                if (request.CurrencyExchangeRateId < 0)
                {
                    result.AddError("Debe ingresar un identificador existente del Tipo de Cambio existente.");
                    return result;
                }

                if (request.Exchange < 0)
                {
                    result.AddError("El tipo de cambio no deber ser igual o menor a 0.");
                    return result;
                }

                var currencyExchangeRateToUpdate = _currencyExchangeRateRepository.FindBy(x => x.Id == request.CurrencyExchangeRateId && x.EsActive && !x.EsEliminado, false).FirstOrDefault();
                if (currencyExchangeRateToUpdate is null)
                {
                    result.AddError("Debe ingresar un identificador existente del Tipo de Cambio existente.");
                    return result;
                }

                currencyExchangeRateToUpdate!.SetNewExchange(request.Exchange);
                currencyExchangeRateToUpdate!.SetUpdateAudit(DateTime.Now, request.RegistredBy);

                _currencyExchangeRateRepository.Update(currencyExchangeRateToUpdate);
                var saved = await _unitOfWork.SaveChangesAsync();
                return result;
            }
        }
    }
}
