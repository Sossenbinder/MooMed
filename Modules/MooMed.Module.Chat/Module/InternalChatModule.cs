using Autofac;
using MooMed.Module.Chat.Database;
using MooMed.Module.Chat.Repository;
using MooMed.Module.Chat.Repository.Interface;
using MooMed.Module.Chat.Service;
using MooMed.Module.Chat.Service.Interface;

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

			builder.RegisterType<ChatMessageRepository>()
				.As<IChatMessageRepository>()
				.SingleInstance();

			builder.RegisterType<MessageService>()
				.As<IMessageService>()
				.SingleInstance();
		}
	}
}
