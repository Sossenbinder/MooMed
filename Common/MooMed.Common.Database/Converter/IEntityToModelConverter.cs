using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Converter
{
	public interface IEntityToModelConverter<in TEntity, out TModel, TKeyType>
		where TModel : IModel
		where TEntity : IEntity<TKeyType>
	{
		TModel ToModel([NotNull] TEntity entity);
	}
}
