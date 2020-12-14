using System.Threading.Tasks;
using MooMed.Eventing.Events;
using MooMed.TestBase;
using NUnit.Framework;

namespace MooMed.Eventing.Tests
{
    public class LocalEventTests : MooMedTestBase
    {
        protected override void Setup()
        {
            base.Setup();
        }

        [Test]
        public void DoesLocalEventRegisterCorrectly()
        {
            var localEvent = new ServiceLocalMooEvent<string>();

            localEvent.Register(TestHandler);
        }

        private Task TestHandler(string str) => Task.CompletedTask;
    }
}