using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace MooMed.Core.Code.Tasks
{
	public static class BackgroundTask
	{
		public static Task Run([NotNull] Func<Task> taskGenerator)
		{
			return Task.Run(taskGenerator);
		}
	}
}
