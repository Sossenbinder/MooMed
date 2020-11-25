using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Common.Database.Converter
{
    public interface IModelToEntityConverter<in TModel, out TEntity, TKeyType>
        where TModel : IModel
        where TEntity : IEntity<TKeyType>
    {
        TEntity ToEntity(TModel model, ISessionContext sessionContext = null!);
    }
}