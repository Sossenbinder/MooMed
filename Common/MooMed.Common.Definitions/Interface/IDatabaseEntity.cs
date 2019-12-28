using JetBrains.Annotations;

namespace MooMed.Common.Definitions.Interface
{
	public interface IDatabaseEntity
	{
		[NotNull]
		string GetKey();
	}
}
