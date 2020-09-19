using Autofac;
using JetBrains.Annotations;
using MooMed.AspNetCore.Modules;
using MooMed.Module.Session.Cache;
using MooMed.Module.Session.Cache.Interface;

namespace MooMed.SessionService.Module
{
    public class SessionServiceModule : Autofac.Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SessionContextCache>()
	            .As<ISessionContextCache>()
	            .SingleInstance();
        }
    }
}
