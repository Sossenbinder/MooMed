﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Core.DataTypes;
using MooMed.DotNet.Utils.ResetLazy;
using MooMed.Module.Finance.Database.Entities;
using MooMed.Module.Finance.Repository.Interface;
using MooMed.Module.Finance.Service.Interface;

namespace MooMed.Module.Finance.Service
{
    public class ExchangeTradedsService : IExchangeTradedsService
    {
        [NotNull]
        private readonly IExchangeTradedRepository _exchangeTradedRepository;

        [NotNull]
        private readonly IEntityToModelConverter<ExchangeTradedEntity, ExchangeTraded, string> _exchangeTradedEntityToModelConverter;

        [NotNull]
        private readonly AsyncResetLazy<List<ExchangeTraded>> _exchangeTradedsCache;

        public ExchangeTradedsService(
            [NotNull] IExchangeTradedRepository exchangeTradedRepository,
            [NotNull] IEntityToModelConverter<ExchangeTradedEntity, ExchangeTraded, string> exchangeTradedEntityToModelConverter)
        {
            _exchangeTradedRepository = exchangeTradedRepository;
            _exchangeTradedEntityToModelConverter = exchangeTradedEntityToModelConverter;

            _exchangeTradedsCache = new AsyncResetLazy<List<ExchangeTraded>>(InitializeExchangeTradedModels);
        }

        private async Task<List<ExchangeTraded>> InitializeExchangeTradedModels()
        {
            var exchangeTradedEntities = await _exchangeTradedRepository.Read();

            return exchangeTradedEntities.ConvertAll(x => _exchangeTradedEntityToModelConverter.ToModel(x));
        }

        public async Task<ServiceResponse<IEnumerable<ExchangeTraded>>> GetExchangeTradeds()
        {
            var models = await _exchangeTradedsCache.Value();

            return ServiceResponse<IEnumerable<ExchangeTraded>>.Success(models);
        }
    }
}