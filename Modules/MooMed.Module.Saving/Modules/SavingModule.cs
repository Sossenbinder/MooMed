using Autofac;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Module.Saving.Converters;
using MooMed.Module.Saving.DataTypes.UiModels;

namespace MooMed.Module.Saving.Modules
{
    public class SavingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SavingModelToUiModelConverter>()
                .As<SavingModelToUiModelConverter, IModelToUiModelConverter<SavingInfoModel, SavingInfoUiModel>>()
                .SingleInstance();
        }
    }
}