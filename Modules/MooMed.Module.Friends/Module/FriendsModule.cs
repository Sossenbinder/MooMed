using Autofac;
using MooMed.Module.Friends.Service;
using MooMed.Module.Friends.Service.Interface;

namespace MooMed.Module.Friends.Module
{
	public class FriendsModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<IFriendsService>()
				.As<FriendsService>()
				.SingleInstance();
		}
	}
}
