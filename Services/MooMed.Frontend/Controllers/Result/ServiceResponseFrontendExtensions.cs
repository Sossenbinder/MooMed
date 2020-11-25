using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Interface;
using MooMed.Core.DataTypes;

namespace MooMed.Frontend.Controllers.Result
{
    public static class ServiceResponseFrontendExtensions
    {
        [NotNull]
        public static JsonResponse ToJsonResponse([NotNull] this ServiceResponse serviceResponse)
            => JsonResponse.Success(serviceResponse.IsSuccess);

        [NotNull]
        public static JsonDataResponse<TPayload> ToJsonResponse<TPayload>([NotNull] this ServiceResponse<TPayload> serviceResponse)
            => JsonResponse.Success(serviceResponse.PayloadOrNull, serviceResponse.IsSuccess);

        [NotNull]
        public static JsonDataResponse<TUiModel> ToUiModelJsonResponse<TPayload, TUiModel>(
            [NotNull] this ServiceResponse<TPayload> serviceResponse,
            [NotNull] IModelToUiModelConverter<TPayload, TUiModel> uiModelConverter)
            where TPayload : IModel
            where TUiModel : IUiModel
        {
            var payloadAsUiModel = uiModelConverter.ToUiModel(serviceResponse.PayloadOrNull);
            return JsonResponse.Success(payloadAsUiModel, serviceResponse.IsSuccess);
        }

        [NotNull]
        public static JsonDataResponse<List<TUiModel>> ToUiModelJsonResponse<TPayload, TUiModel>(
            [NotNull] this ServiceResponse<IEnumerable<TPayload>> serviceResponse,
            [NotNull] IModelToUiModelConverter<TPayload, TUiModel> uiModelConverter)
            where TPayload : IModel
            where TUiModel : IUiModel
        {
            var uiModels = serviceResponse.PayloadOrNull;

            return ToUiModelJsonResponse(uiModels, serviceResponse.IsSuccess, uiModelConverter);
        }

        [NotNull]
        public static JsonDataResponse<List<TUiModel>> ToUiModelJsonResponse<TPayload, TUiModel>(
            [NotNull] this ServiceResponse<List<TPayload>> serviceResponse,
            [NotNull] IModelToUiModelConverter<TPayload, TUiModel> uiModelConverter)
            where TPayload : IModel
            where TUiModel : IUiModel
        {
            var uiModels = serviceResponse.PayloadOrNull;

            return ToUiModelJsonResponse(uiModels, serviceResponse.IsSuccess, uiModelConverter);
        }

        [NotNull]
        private static JsonDataResponse<List<TUiModel>> ToUiModelJsonResponse<TPayload, TUiModel>(
            [NotNull] IEnumerable<TPayload> uiModels,
            bool successState,
            [NotNull] IModelToUiModelConverter<TPayload, TUiModel> uiModelConverter)
            where TPayload : IModel
            where TUiModel : IUiModel
        {
            var models = uiModels.ToList().ConvertAll(uiModelConverter.ToUiModel);

            return JsonResponse.Success(models, successState);
        }
    }
}