using Autofac;

namespace MooMed.Module.Chat.Module
{
	public class ChatModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterModule<InternalChatModule>();
		}
	}
}
