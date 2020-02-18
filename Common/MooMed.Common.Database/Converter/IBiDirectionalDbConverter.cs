using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Converter
{
	public interface IBiDirectionalDbConverter<TModel, TEntity> : IEntityConverter<TModel, TEntity>, IModelConverter<TModel, TEntity>
		where TModel : IModel
		where TEntity : IEntity
	{
	}
}
