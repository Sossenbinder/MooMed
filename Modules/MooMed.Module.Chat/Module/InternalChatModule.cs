using Autofac;
using MooMed.Module.Chat.Database;

namespace MooMed.Module.Chat.Module
{
	public class InternalChatModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<ChatDbContextFactory>()
				.AsSelf()
				.SingleInstance()
				.WithParameter("key", "MooMed_Database_Account");
		}
	}
}
