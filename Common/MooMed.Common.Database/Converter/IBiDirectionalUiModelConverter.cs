using System;
using System.Collections.Generic;
using System.Text;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Converter
{
	public interface IBiDirectionalUiModelConverter<TUiModel, TModel> : IModelToUiModelConverter<TModel, TUiModel>, IUiModelToModelConverter<TUiModel, TModel>
		where TModel : IModel
		where TUiModel : IUiModel
	{ }
}
