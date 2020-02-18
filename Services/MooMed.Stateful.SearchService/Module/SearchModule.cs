using Autofac;
using MooMed.AspNetCore.Modules;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Stateful.SearchService.Remoting;

namespace MooMed.Stateful.SearchService.Module
{
    public class SearchModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
	        base.Load(builder);
        }
    }
}
