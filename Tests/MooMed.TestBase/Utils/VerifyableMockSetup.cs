using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;

namespace MooMed.TestBase.Utils
{
    public class VerifiableMockSetup
    {
        private Action<Times?> _setupVerifier;

        private VerifiableMockSetup(Action<Times?> setupVerifier)
        {
            _setupVerifier = setupVerifier;
        }

        public static VerifiableMockSetup Setup<TIn>(
            Mock<TIn> mock,
            Expression<Action<TIn>> moqSetup)
            where TIn : class
        {
            mock.Setup(moqSetup);

            return new VerifiableMockSetup(times => mock.Verify(moqSetup, times ?? Times.AtLeastOnce()));
        }

        public static VerifiableMockSetup Setup<TIn, TOut>(
            Mock<TIn> mock,
            Expression<Func<TIn, TOut>> moqSetup,
            TOut returnVal)
            where TIn : class
        {
            mock.Setup(moqSetup)
                .Returns(returnVal);

            return new VerifiableMockSetup(times => mock.Verify(moqSetup, times ?? Times.AtLeastOnce()));
        }

        public static VerifiableMockSetup Setup<TIn, TOut>(
            Mock<TIn> mock,
            Expression<Func<TIn, TOut>> moqSetup,
            Func<TIn, TOut> returnGenerator)
            where TIn : class
        {
            mock.Setup(moqSetup)
                .Returns(returnGenerator);

            return new VerifiableMockSetup(times => mock.Verify(moqSetup, times ?? Times.AtLeastOnce()));
        }

        public static VerifiableMockSetup SetupAsync<TIn, TOut>(
            Mock<TIn> mock,
            Expression<Func<TIn, Task<TOut>>> moqSetup,
            Task<TOut> returnVal)
            where TIn : class
            => Setup(mock, moqSetup, returnVal);

        public void Verify() => _setupVerifier(Times.AtLeastOnce());

        public void Verify(Times times) => _setupVerifier(times);

        public void Verify(Func<Times> times) => _setupVerifier(times());
    }
}