using Autofac;
using MooMed.AspNetCore.Modules;
using MooMed.SearchService.Remoting;

namespace MooMed.SearchService.Module
{
    public class SearchModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
	        base.Load(builder);
        }
    }
}
