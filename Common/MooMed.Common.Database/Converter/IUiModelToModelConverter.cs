using System.Diagnostics.CodeAnalysis;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Converter
{
	public interface IUiModelToModelConverter<in TUiModel, out TModel>
		where TUiModel : IUiModel
		where TModel : IModel
	{
		TModel ToModel([NotNull] TUiModel uiModel);
	}
}
