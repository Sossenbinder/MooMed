using Autofac;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Module.Search.Service;

namespace MooMed.Module.Search
{
    public class SearchBindings : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SearchService>()
                .As<ISearchService>()
                .SingleInstance();
        }
    }
}
