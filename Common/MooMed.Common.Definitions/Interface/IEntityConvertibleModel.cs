using JetBrains.Annotations;

namespace MooMed.Common.Definitions.Interface
{
	interface IEntityConvertibleModel<out TEntityType> 
		where TEntityType : IDatabaseEntity
	{
		[NotNull]
		TEntityType ToEntity();
	}
}
