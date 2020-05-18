﻿using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MooMed.Dns.Service.Interface
{
	public interface IDnsResolutionService
	{
		Task<IPAddress> ResolveDnsNameToIp([NotNull] string name);

		string GetOwnHostName();
	}
}
