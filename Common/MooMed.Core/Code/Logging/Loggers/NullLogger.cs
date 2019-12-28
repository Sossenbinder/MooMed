using MooMed.Common.Definitions.Models.Session;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;

namespace MooMed.Core.Code.Logging.Loggers
{
    public class NullMooMedLogger : IMainLogger
    {
	    public void Info(string message, ISessionContext sessionContext)
	    {

	    }

        public void Warning(string message, ISessionContext sessionContext)
        {

        }

		public void Error(string message, ISessionContext sessionContext)
		{

		}

		public void Fatal(string message, ISessionContext sessionContext)
		{

		}

		public void System(string message, ISessionContext sessionContext)
		{

		}
	}
}
