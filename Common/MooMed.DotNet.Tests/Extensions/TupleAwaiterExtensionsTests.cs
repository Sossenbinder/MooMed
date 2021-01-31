using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MooMed.DotNet.Extensions.Tuple;
using NUnit.Framework;

namespace MooMed.DotNet.Tests.Extensions
{
	[TestFixture]
	public class TupleAwaiterExtensionsTests
	{
		[Test]
		public async Task AwaitingTupleWith2EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 2;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 2; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

		[Test]
		public async Task AwaitingTupleWith3EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 3;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 3; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

		[Test]
		public async Task AwaitingTupleWith4EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 4;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 4; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

		[Test]
		public async Task AwaitingTupleWith5EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 5;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 5; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

		[Test]
		public async Task AwaitingTupleWith6EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 6;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 6; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

		[Test]
		public async Task AwaitingTupleWith7EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 7;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 7; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

		[Test]
		public async Task AwaitingTupleWith8EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 8;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 8; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

		[Test]
		public async Task AwaitingTupleWith9EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 9;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 9; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

		[Test]
		public async Task AwaitingTupleWith10EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 10;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 10; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

		[Test]
		public async Task AwaitingTupleWith11EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 11;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 11; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

		[Test]
		public async Task AwaitingTupleWith12EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 12;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 12; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

		[Test]
		public async Task AwaitingTupleWith13EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 13;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 13; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

		[Test]
		public async Task AwaitingTupleWith14EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 14;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 14; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

		[Test]
		public async Task AwaitingTupleWith15EntriesShouldWorkForHappyPath()
		{
			const int verificationNumber = 15;

			var tuple = (Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber), Task.FromResult(verificationNumber));

			ITuple results = await tuple;

			for (var i = 0; i < 15; ++i)
			{
				Assert.AreEqual(verificationNumber, results[i]);
			}
		}

	}
}