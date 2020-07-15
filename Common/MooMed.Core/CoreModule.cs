using Autofac;
using JetBrains.Annotations;
using MooMed.Core.Code.Helper.Email;
using MooMed.Core.Code.Helper.Email.Interface;

namespace MooMed.Core
{
    public class CoreModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
	        builder.RegisterType<EmailManager>()
                .As<IEmailManager>()
                .SingleInstance();
        }
    }
}
