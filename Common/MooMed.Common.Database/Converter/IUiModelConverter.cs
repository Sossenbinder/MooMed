using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Converter
{
	public interface IUiModelConverter<out TUiModel, in TModel>
		where TUiModel : IUiModel
		where TModel : IModel
	{
		TUiModel ToUiModel([NotNull] TModel model);
	}
}
