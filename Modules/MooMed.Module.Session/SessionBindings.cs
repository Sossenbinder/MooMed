using Autofac;
using JetBrains.Annotations;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Module.Session.Service;

namespace MooMed.Module.Session
{
    public class SessionBindings : Autofac.Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            builder.RegisterType<SessionService>()
                .As<ISessionService>()
                .SingleInstance();
        }
    }
}
