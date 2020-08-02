using System.Collections.Generic;
using System.Threading.Tasks;
using MooMed.AspNetCore.Grpc.Serialization;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Definitions.Interface;
using Moq;
using NUnit.Framework;
using ProtoBuf.Meta;

namespace MooMed.AspNetCore.Tests.Serialization
{
	public interface ITestGrpcService : IGrpcService
	{
		ServiceResponse<int> MethodOne(Task<int> param);

		ServiceResponse<int> MethodTwo();

		ServiceResponse<string> MethodThree(bool param);
	}

	[TestFixture]
	public class GrpcServiceTypeSerializerTests
	{
		[Test]
		public void GrpcServiceTypeSerializerShouldWorkForHappyPath()
		{
			var toTest = new GrpcServiceTypeSerializer(new SerializationHelper());

			toTest.SerializeGrpcServices(new List<IGrpcService>()
			{
				Mock.Of<ITestGrpcService>()
			});

			var typeModel = RuntimeTypeModel.Default;

			var registeredTypesEnumerator = typeModel.GetTypes().GetEnumerator();

			while (registeredTypesEnumerator.MoveNext())
			{
				var metaType = registeredTypesEnumerator.Current as MetaType;

				if (metaType == null)
				{
					continue;
				}

				if (metaType.Type == typeof(ServiceResponse))
				{
					Assert.IsTrue(metaType.Type.BaseType == typeof(ServiceResponseBase));
				}
			}
		}
	}
}