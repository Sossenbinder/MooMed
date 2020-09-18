using System;
using MooMed.Common.Definitions.IPC;

namespace MooMed.ServiceBase.Attributes
{
	public class KubernetesServiceTypeAttribute : Attribute
	{
		public ServiceType ServiceType { get; }

		public KubernetesServiceTypeAttribute(ServiceType serviceType)
		{
			ServiceType = serviceType;
		}
	}
}