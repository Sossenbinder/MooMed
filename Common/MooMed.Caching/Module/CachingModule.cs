using Autofac;
using MooMed.Caching.Cache.CacheInformation;
using MooMed.Caching.Cache.Factory;

namespace MooMed.Caching.Module
{
	public class CachingModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			
			builder.RegisterType<CacheSettingsProvider>()
				.As<CacheSettingsProvider>()
				.SingleInstance();

			builder.RegisterType<ObjectCacheFactory>()
				.As<IDefaultCacheFactory>()
				.SingleInstance();
		}
	}
}
