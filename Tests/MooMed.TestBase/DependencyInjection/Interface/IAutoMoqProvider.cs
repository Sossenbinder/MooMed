using System;
using Autofac.Core;

namespace MooMed.TestBase.DependencyInjection.Interface
{
    public interface IAutoMoqProvider : IDisposable
    {
        object? CreateMock(Service service);
    }
}