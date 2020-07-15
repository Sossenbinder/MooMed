using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace MooMed.DotNet.Utils.Tasks
{
	public static class FireAndForgetTask
	{
		public static Task Run([NotNull] Func<Task> taskGenerator)
		{
			return Task.Run(taskGenerator);
		}
	}
}
