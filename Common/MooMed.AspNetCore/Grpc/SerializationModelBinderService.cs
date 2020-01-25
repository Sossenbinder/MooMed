using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.ServiceBase;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.Code.Extensions;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Definitions.Interface;
using ProtoBuf.Meta;

namespace MooMed.AspNetCore.Grpc
{
	public class SerializationModelBinderService : IStartable
	{
		[NotNull]
		private readonly IEnumerable<Type> m_grpServices;

		private int m_protoIndex = 50;

		public SerializationModelBinderService()
		{
			m_grpServices = AppDomain.CurrentDomain.GetAssemblies()
				.Single(x => x.FullName.Contains("MooMed.Common.ServiceBase"))
				.GetTypes()
				.Where(x => x != typeof(MooMedServiceBase));
		}

		private void InitializeBindingsForGrpcService([NotNull] Type grpService)
		{
			foreach (var method in grpService.GetMethods())
			{
				var involvedTypes = method.GetParameters().Select(x => x.ParameterType).ToList();
				involvedTypes.Add(method.ReturnType);

				var genericTypes = involvedTypes.Where(x => x.IsGenericType);

				var metaData = RuntimeTypeModel.Default.Add<ServiceResponseBase>();
				foreach (var type in genericTypes)
				{
					var unWrappedType = type.CheckAndGetTaskWrappedType();

					if (unWrappedType.BaseType == typeof(ServiceResponseBase))
					{
						metaData.AddSubType(m_protoIndex, unWrappedType);
					}
					m_protoIndex++;
				}

				var nongenerics = involvedTypes.Where(x => !x.IsGenericType);

				foreach (var type in nongenerics)
				{
					if (!type.IsPrimitive && type.FullName != null && !type.FullName.Equals("System.String"))
					{
						RuntimeTypeModel.Default.Add(type, true);
						m_protoIndex++;
					}
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
