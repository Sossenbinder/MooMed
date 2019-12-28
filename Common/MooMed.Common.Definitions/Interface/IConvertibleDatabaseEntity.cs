using JetBrains.Annotations;

namespace MooMed.Common.Definitions.Interface
{
	public interface IConvertibleDatabaseEntity<out TModelType> : IDatabaseEntity
	{
		[NotNull]
		TModelType ToModel();
	}
}
