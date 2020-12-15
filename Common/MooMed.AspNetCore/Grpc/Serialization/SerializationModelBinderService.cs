using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Autofac;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.User;
using MooMed.DotNet.Extensions;
using MooMed.Logging.Abstractions.Interface;
using MooMed.ServiceBase.Definitions.Interface;
using ProtoBuf.Meta;

namespace MooMed.AspNetCore.Grpc.Serialization
{
    /// <summary>
    /// Auto-binds all return values & params used by grpc service
    /// </summary>
    public class SerializationModelBinderService : IStartable
    {
        private readonly IMooMedLogger _logger;

        private readonly IEnumerable<IGrpcService> _services;

        private readonly GrpcServiceTypeSerializer _grpcServiceTypeSerializer;

        private readonly SerializationHelper _serializationHelper;

        public SerializationModelBinderService(
            IMooMedLogger logger,
            IEnumerable<IGrpcService> services,
            GrpcServiceTypeSerializer grpcServiceTypeSerializer,
            SerializationHelper serializationHelper)
        {
            _logger = logger;
            _services = services;
            _grpcServiceTypeSerializer = grpcServiceTypeSerializer;
            _serializationHelper = serializationHelper;
        }

        private void BindSessionContextAttachedContainers()
        {
            var containerType = typeof(SessionContextAttachedContainer);
            var index = 700;

            var containerImplementers = Assembly
                .GetAssembly(containerType)
                ?.GetTypes()
                .Where(x => x.BaseType == containerType).ToList();

            if (containerImplementers.IsNullOrEmpty())
            {
                return;
            }

            var baseMetaData = RuntimeTypeModel.Default.Add(containerType);

            foreach (var implementer in containerImplementers)
            {
                baseMetaData.AddSubType(index, implementer);
                index++;
            }
        }

        public void Start()
        {
            var stopWatch = Stopwatch.StartNew();

            _grpcServiceTypeSerializer.SerializeGrpcServices(_services);

            BindSessionContextAttachedContainers();

            _serializationHelper.SerializeType<Account>();

            RuntimeTypeModel.Default[typeof(DateTimeOffset?)].SetSurrogate(typeof(DateTimeOffsetSurrogate));

            stopWatch.Stop();
            _logger.Info($"Initialization of serialization model service took {stopWatch.Elapsed}");

            RuntimeTypeModel.Default.AutoAddMissingTypes = true;
            RuntimeTypeModel.Default.AutoCompile = true;
        }
    }
}