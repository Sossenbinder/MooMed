using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Converter
{
	public interface IModelToEntityConverter<in TModel, out TEntity, TKeyType>
		where TModel : IModel
		where TEntity : IEntity<TKeyType>
	{
		TEntity ToEntity([NotNull] TModel model);
	}
}
