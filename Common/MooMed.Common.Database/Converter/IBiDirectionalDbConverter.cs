using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Converter
{
	public interface IBiDirectionalDbConverter<TModel, TEntity, TKeyType> : IEntityConverter<TModel, TEntity, TKeyType>, IModelConverter<TModel, TEntity, TKeyType>
		where TModel : IModel
		where TEntity : IEntity<TKeyType>
	{
	}
}
