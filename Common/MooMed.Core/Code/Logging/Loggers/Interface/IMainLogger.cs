using System.Diagnostics;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Core.Code.Logging.Loggers.Interface
{
    public interface IMainLogger
    {
	    void Info([NotNull] string message);

	    void Info([NotNull] string message, int accountId);

        void Info([NotNull] string message, [CanBeNull] ISessionContext sessionContext);

        void Debug([NotNull] string message);

        void Debug([NotNull] string message, int accountId);

        void Debug([NotNull] string message, [CanBeNull] ISessionContext sessionContext);

        void Warning([NotNull] string message);

        void Warning([NotNull] string message, int accountId);

        void Warning([NotNull] string message, [CanBeNull] ISessionContext sessionContext);

        void Error([NotNull] string message);

        void Error([NotNull] string message, int accountId);

        void Error([NotNull] string message, [CanBeNull] ISessionContext sessionContext);

        void Fatal([NotNull] string message);

        void Fatal([NotNull] string message, int accountId);

        void Fatal([NotNull] string message, [CanBeNull] ISessionContext sessionContext);
    }
}
