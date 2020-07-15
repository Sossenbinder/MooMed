using System;
using System.Threading;
using System.Threading.Tasks;

namespace MooMed.DotNet.Utils.Tasks
{
	public struct RestartingBackgroundTaskOptions
	{
		public  string Name { get; set; }

		public CancellationToken? CancellationToken { get; set; }

		public Func<Exception, Task> OnException { get; set; }
	}
}
