using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using JetBrains.Annotations;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Definitions.Interface;
using ProtoBuf.Meta;
using MooMed.Core.Code.Extensions;

namespace MooMed.Core.Code.Utils
{
	public class SerializationModelBinderService : IStartable
	{
		[NotNull]
		private readonly IEnumerable<Type> m_grpServices;

		public SerializationModelBinderService()
		{
			m_grpServices = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => x.IsInterface && x.GetInterfaces().Contains(typeof(IGrpcService)));
		}

		private void InitializeBindingsForGrpcService([NotNull] Type grpService)
		{
			var protoIndex = 1000;
			foreach (var method in grpService.GetMethods())
			{
				var involvedTypes = method.GetParameters().Select(x => x.ParameterType).ToList();
				involvedTypes.Add(method.ReturnType);

				foreach (var type in involvedTypes.Where(x => x.IsGenericType))
				{
					var unWrappedType = type.CheckAndGetTaskWrappedType();

					if (unWrappedType.BaseType == typeof(ServiceResponseBase))
					{
						RuntimeTypeModel.Default.Add(typeof(ServiceResponseBase), true)
							.AddSubType(protoIndex, unWrappedType);
					}
					protoIndex++;
				}
			}
		}

		public void Start()
		{
			foreach (var grpcService in m_grpServices)
			{
				InitializeBindingsForGrpcService(grpcService);
			}
		}
	}
}
