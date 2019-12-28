﻿using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace MooMed.AspNetCore.Extensions
{
	public static class IWebHostBuilderExtensions
	{
		[NotNull]
		public static IWebHostBuilder ConfigureGrpc([NotNull] this IWebHostBuilder webHostBuilder, int port = 10042)
		{
			webHostBuilder
				.UseKestrel()
				.ConfigureKestrel(options =>
				{
					options.ListenAnyIP(10042, listenOptions =>
					{
						listenOptions.Protocols = HttpProtocols.Http2; 
					});
				});

			return webHostBuilder;
		}
	}
}