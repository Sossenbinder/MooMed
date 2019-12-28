using System;
using System.Threading.Tasks;
using MooMed.Core.Code.Events;
using MooMed.TestBase;
using NUnit.Framework;

namespace MooMed.Core.Tests.Tests.Events
{
    [TestFixture]
    public class MooEventTests : MooMedTestBase
    {
        private MooEvent<int> m_mooEvent;

        protected override void Setup()
        {
            base.Setup();

            m_mooEvent = new MooEvent<int>();
        }

        [Test]
        public void RegisteringShouldWork()
        {
            var testFunc = new Func<int, Task>((args) => Task.CompletedTask);

            m_mooEvent.Register(testFunc);

            Assert.NotZero(m_mooEvent.GetAllRegisteredEvents().Count);
        }

        [Test]
        public void UnRegisteringShouldWork()
        {
            var testFunc = new Func<int, Task>((args) => Task.CompletedTask);

            m_mooEvent.Register(testFunc);

            Assert.NotZero(m_mooEvent.GetAllRegisteredEvents().Count);

            m_mooEvent.UnRegister(testFunc);

            Assert.Zero(m_mooEvent.GetAllRegisteredEvents().Count);
        }
    }
}
