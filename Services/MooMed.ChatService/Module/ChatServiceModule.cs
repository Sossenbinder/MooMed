using Autofac;
using MooMed.Module.Chat.Module;

namespace MooMed.ChatService.Module
{
	public class ChatServiceModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterModule<ChatModule>();
		}
	}
}
