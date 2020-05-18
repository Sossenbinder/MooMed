﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.IPC.ProxyInvocation.Interface
{
	public interface IStatefulSetProxy<out TServiceType>
		where TServiceType : IGrpcService
	{
		/// <summary>
		/// Invoke a function on a random replica
		/// </summary>
		/// <param name="invocationFunc">Function to invoke on the endpoint</param>
		/// <returns>Task representing the invocation</returns>
		[NotNull]
		Task InvokeRandom([NotNull] Func<TServiceType, Task> invocationFunc);

		/// <summary>
		/// Invoke a function on a random replica and return a result
		/// </summary>
		/// <param name="invocationFunc">Function to invoke on the endpoint</param>
		/// <returns>Task representing the invocation, containing the result</returns>
		[NotNull]
		Task<TResult> InvokeRandomWithResult<TResult>([NotNull] Func<TServiceType, Task<TResult>> invocationFunc);

		/// <summary>
		/// Invoke a function on a specific replica
		/// </summary>
		/// <param name="endpointSelector">Selector for endpoint</param>
		/// <param name="invocationFunc">Function to invoke on the endpoint</param>
		/// <returns>Task representing the invocation</returns>
		Task InvokeSpecific(IEndpointSelector endpointSelector, Func<TServiceType, Task> invocationFunc);

		/// <summary>
		/// Invoke a function on a specific replica and return a result
		/// </summary>
		/// <param name="endpointSelector">Selector for endpoint</param>
		/// <param name="invocationFunc">Function to invoke on the endpoint</param>
		/// <returns>Task representing the invocation, containing the result</returns>
		Task<TResult> InvokeSpecificWithResult<TResult>(IEndpointSelector endpointSelector, Func<TServiceType, Task<TResult>> invocationFunc);
	}
}