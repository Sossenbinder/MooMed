﻿using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.IPC.Interface;
using MooMed.IPC.EndpointResolution.Interface;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation.Interface;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.IPC.ProxyInvocation
{
	public abstract class AbstractStatefulSetProxy<TServiceType> : IStatefulSetProxy<TServiceType>
		where TServiceType : class, IGrpcService
	{
		[NotNull]
		private readonly IEndpointProvider _endpointProvider;

		[NotNull]
		private readonly ISpecificGrpcClientProvider _clientProvider;

		[NotNull]
		private readonly IDeterministicPartitionSelectorHelper _deterministicPartitionSelectorHelper;

		[NotNull]
		private readonly Random _random;

		private readonly StatefulSetService _moomedService;

		protected AbstractStatefulSetProxy(
			[NotNull] IEndpointProvider endpointProvider,
			[NotNull] ISpecificGrpcClientProvider clientProvider,
			[NotNull] IDeterministicPartitionSelectorHelper deterministicPartitionSelectorHelper,
			StatefulSetService moomedService)
		{
			_endpointProvider = endpointProvider;
			_clientProvider = clientProvider;
			_deterministicPartitionSelectorHelper = deterministicPartitionSelectorHelper;
			_random = new Random();

			_moomedService = moomedService;
		}

		// Random invocation
		public Task InvokeRandom(Func<TServiceType, Task> invocationFunc)
		{
			var proxy = _clientProvider.GetGrpcClient<TServiceType>(
				_moomedService,
				GetRandomReplicaNumber());

			return invocationFunc(proxy);
		}

		public Task<TResult> InvokeRandomWithResult<TResult>(Func<TServiceType, Task<TResult>> invocationFunc)
		{
			var proxy = _clientProvider.GetGrpcClient<TServiceType>(
				_moomedService,
				GetRandomReplicaNumber());

			return invocationFunc(proxy);
		}

		// Specific invocation
		public Task InvokeSpecific(
			IEndpointSelector endpointSelector,
			Func<TServiceType, Task> invocationFunc)
			=> InvokeSpecific(endpointSelector.HashableIdentifier, invocationFunc);

		public Task<TResult> InvokeSpecificWithResult<TResult>(
			IEndpointSelector endpointSelector,
			Func<TServiceType, Task<TResult>> invocationFunc)
			=> InvokeSpecificWithResult(endpointSelector.HashableIdentifier, invocationFunc);

		protected Task InvokeSpecific(
			ISessionContextAttachedContainer hashableIdentifier,
			[NotNull] Func<TServiceType, Task> invocationFunc)
			=> InvokeSpecific(hashableIdentifier.SessionContext.HashableIdentifier, invocationFunc);

		protected Task<TResult> InvokeSpecificWithResult<TResult>(
			ISessionContextAttachedContainer hashableIdentifier,
			[NotNull] Func<TServiceType, Task<TResult>> invocationFunc)
			=> InvokeSpecificWithResult(hashableIdentifier.SessionContext.HashableIdentifier, invocationFunc);

		protected Task InvokeSpecific(
			int hashableIdentifier,
			[NotNull] Func<TServiceType, Task> invocationFunc)
		{
			var proxy = _clientProvider.GetGrpcClient<TServiceType>(
				_moomedService,
				ResolveToReplicaNumber(hashableIdentifier));

			return invocationFunc(proxy);
		}

		protected Task<TResult> InvokeSpecificWithResult<TResult>(
			int hashableIdentifier,
			[NotNull] Func<TServiceType, Task<TResult>> invocationFunc)
		{
			var proxy = _clientProvider.GetGrpcClient<TServiceType>(
				_moomedService,
				ResolveToReplicaNumber(hashableIdentifier));

			return invocationFunc(proxy);
		}

		private int ResolveToReplicaNumber(int hashableIdentifier)
		{
			var replicas = _endpointProvider.GetAvailableReplicasForStatefulService(_moomedService);

			return _deterministicPartitionSelectorHelper.HashIdentifierToPartitionIntIdentifier(hashableIdentifier, replicas);
		}

		private int GetRandomReplicaNumber()
		{
			var replicas = _endpointProvider.GetAvailableReplicasForStatefulService(_moomedService);
			return _random.Next(replicas);
		}
	}
}