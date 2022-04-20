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

namespace Yup.MoneyExchange.Application.Currencies.Commands;

public class CreateCurrencyCommand : IRequest<GenericResult>
{
    /// <summary>
    /// Nombre de la moneda
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Abreviatura de la moneda
    /// </summary>
    public string Abreviature { get; set; }
    [JsonIgnore]
    public Guid RegistredBy { get; set; }
    public CreateCurrencyCommand(string name, string abreviature, Guid registredBy)
    {
        Name = name;
        Abreviature = abreviature;
        RegistredBy = registredBy;
    }

    public class CreateCurrencyCommandCommandHandler : IRequestHandler<CreateCurrencyCommand, GenericResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Currency> _currencyRepository;

        public CreateCurrencyCommandCommandHandler(IUnitOfWork unitOfWork, IBaseRepository<Currency> currencyRepository)
        {
            _unitOfWork = unitOfWork;
            _currencyRepository = currencyRepository;
        }

        public async Task<GenericResult> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var result = new GenericResult();
            //Validaciones
            //TODO: Llevar a FluentValidation

            var currencyToSave = new Currency(request.Name, request.Abreviature);

            currencyToSave.SetCreateAudit(DateTime.Now, request.RegistredBy);
            var resultAdd = _currencyRepository.Add(currencyToSave);

            var saved = await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }

}
