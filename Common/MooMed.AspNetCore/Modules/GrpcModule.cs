﻿using Autofac;
using MooMed.AspNetCore.Grpc;

namespace MooMed.AspNetCore.Modules
{
	public class GrpcModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<SerializationModelBinderService>()
				.As<IStartable>()
				.SingleInstance();
		}
	}
}