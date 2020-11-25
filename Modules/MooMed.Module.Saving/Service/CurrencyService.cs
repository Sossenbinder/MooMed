using System;
using System.Linq;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Module.Saving.Database.Entities;
using MooMed.Module.Saving.Repository.Interface;
using MooMed.Module.Saving.Service.Interface;

namespace MooMed.Module.Saving.Service
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyMappingRepository _currencyMappingRepository;

        public CurrencyService(ICurrencyMappingRepository currencyMappingRepository)
        {
            _currencyMappingRepository = currencyMappingRepository;
        }

        public async Task SetCurrency(ISessionContext sessionContext, Currency currency)
        {
            var entity = new CurrencyMappingEntity()
            {
                Id = sessionContext.Account.Id,
                Currency = currency
            };

            await _currencyMappingRepository.CreateOrUpdate(entity, x => x.Currency = currency);
        }

        public async Task<Currency?> GetCurrency(ISessionContext sessionContext)
        {
            var result = await _currencyMappingRepository.Read(x => x.Id == sessionContext.Account.Id);

            if (result.Count == 0)
            {
                return null;
            }

            return result.Single().Currency;
        }
    }
}