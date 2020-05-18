using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MooMed.IPC.DataType
{
	public class StatefulEndpointCollection
	{
		public int ReplicaCount => Endpoints.Count;

		public List<StatefulEndpoint> Endpoints { get; set; }

		public StatefulEndpointCollection([NotNull] List<StatefulEndpoint> endpoints)
		{
			Endpoints = endpoints;
		}
	}
}
