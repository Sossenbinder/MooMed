using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Converter
{
    public interface IBiDirectionalDbConverter<TModel, TEntity, TKeyType> : IModelToEntityConverter<TModel, TEntity, TKeyType>, IEntityToModelConverter<TEntity, TModel, TKeyType>
        where TModel : IModel
        where TEntity : IEntity<TKeyType>
    {
    }
}