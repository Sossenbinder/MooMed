using JetBrains.Annotations;

namespace MooMed.Common.Definitions.Interface
{
	public interface IEntity
	{
		[NotNull]
		string GetKey();
	}
}
