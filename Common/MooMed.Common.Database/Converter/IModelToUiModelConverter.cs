using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Converter
{
    public interface IModelToUiModelConverter<in TModel, out TUiModel>
        where TUiModel : IUiModel
        where TModel : IModel
    {
        TUiModel ToUiModel(TModel model);
    }
}