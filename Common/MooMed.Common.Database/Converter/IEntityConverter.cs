using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Converter
{
	public interface IEntityConverter<in TModel, out TEntity>
		where TModel : IModel
		where TEntity : IEntity
	{
		TEntity ToEntity([NotNull] TModel model);
	}
}
