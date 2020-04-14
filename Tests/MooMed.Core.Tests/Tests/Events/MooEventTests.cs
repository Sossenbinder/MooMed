using System;
using System.Threading.Tasks;
using MooMed.Eventing.Events;
using MooMed.TestBase;
using NUnit.Framework;

namespace MooMed.Core.Tests.Tests.Events
{
    [TestFixture]
    public class MooEventTests : MooMedTestBase
    {
        private ServiceLocalMooEvent<int> m_serviceLocalMooEvent;

        protected override void Setup()
        {
            base.Setup();

            m_serviceLocalMooEvent = new ServiceLocalMooEvent<int>();
        }

        [Test]
        public void RegisteringShouldWork()
        {
            var testFunc = new Func<int, Task>((args) => Task.CompletedTask);

            m_serviceLocalMooEvent.Register(testFunc);

            Assert.NotZero(m_serviceLocalMooEvent.GetAllRegisteredEvents().Count);
        }

        [Test]
        public void UnRegisteringShouldWork()
        {
            var testFunc = new Func<int, Task>((args) => Task.CompletedTask);

            m_serviceLocalMooEvent.Register(testFunc);

            Assert.NotZero(m_serviceLocalMooEvent.GetAllRegisteredEvents().Count);

            m_serviceLocalMooEvent.UnRegister(testFunc);

            Assert.Zero(m_serviceLocalMooEvent.GetAllRegisteredEvents().Count);
        }
    }
}
