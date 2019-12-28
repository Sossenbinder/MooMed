namespace MooMed.Common.Definitions.Interface
{
	internal interface IBiDirectionalConvertibleModel<out TEntityType, out TUiModel> : IEntityConvertibleModel<TEntityType>, IUiModelConvertibleModel<TUiModel> 
		where TEntityType : IDatabaseEntity
	{
	}
}
