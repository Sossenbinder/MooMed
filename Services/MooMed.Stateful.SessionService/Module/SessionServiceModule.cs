using Autofac;
using JetBrains.Annotations;

namespace MooMed.Stateful.SessionService.Module
{
    public class SessionServiceModule : Autofac.Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
