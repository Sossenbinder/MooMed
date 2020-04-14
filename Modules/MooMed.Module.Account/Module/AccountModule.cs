using Autofac;
using JetBrains.Annotations;
using MooMed.Module.Accounts.Events;
using MooMed.Module.Accounts.Events.Interface;

namespace MooMed.Module.Accounts.Module
{
    public class AccountModule : Autofac.Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder.RegisterType<AccountEventHub>()
                .As<IAccountEventHub>()
                .SingleInstance();
        }
    }
}
