using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Core.Code.Logging.Loggers.Interface
{
    public interface IMainLogger
    {
        void Info([NotNull] string message, [CanBeNull] ISessionContext sessionContext);

        void Warning([NotNull] string message, [CanBeNull] ISessionContext sessionContext);

        void Error([NotNull] string message, [CanBeNull] ISessionContext sessionContext);

        void Fatal([NotNull] string message, [CanBeNull] ISessionContext sessionContext);

        void System([NotNull] string message, [CanBeNull] ISessionContext sessionContext);
    }
}
