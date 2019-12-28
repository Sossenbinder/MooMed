
using JetBrains.Annotations;

namespace MooMed.Common.Definitions.Interface
{
	interface IUiModelConvertibleModel<out TUiModelType>
	{
		[NotNull]
		TUiModelType ToUiModel();
	}
}
