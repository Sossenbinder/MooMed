using System;
using System.Threading.Tasks;
using MooMed.DotNet.Extensions;
using NUnit.Framework;

namespace MooMed.DotNet.Tests.Extensions
{
	[TestFixture]
	public class ActionExtensionsTests
	{
		[Test]
		public async Task NonGenericActionShouldProperlyWorkWhenConverted()
		{
			var called = false;

			Action action = () => called = true;

			Func<Task> convertedAction = action.MakeTaskCompatible()!;

			await convertedAction();

			Assert.True(called);
		}

		[Test]
		public async Task GenericActionShouldProperlyWorkWhenConverted()
		{
			const int number = 5;
			var verifyNr = 0;

			Action<int> action = nr => verifyNr = nr;

			Func<int, Task> convertedAction = action.MakeTaskCompatible()!;

			await convertedAction(number);

			Assert.True(verifyNr == number);
		}
	}
}