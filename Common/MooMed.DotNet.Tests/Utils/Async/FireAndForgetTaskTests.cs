using MooMed.DotNet.Utils.Tasks;
using NUnit.Framework;

namespace MooMed.DotNet.Tests.Utils.Async
{
	[TestFixture]
	public class FireAndForgetTaskTests
	{
		[Test]
		public void FireAndForgetTaskShouldWorkForHappyPath()
		{
			var ffTask = FireAndForgetTask.Run()
		}
	}
}