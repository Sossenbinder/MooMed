using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Converter
{
	public interface IModelConverter<out TModel, in TEntity>
		where TModel : IModel
		where TEntity : IEntity
	{
		TModel ToModel([NotNull] TEntity entity);
	}
}
