using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.Code.Logging.Loggers.Interface;

namespace MooMed.Core.Code.Logging.Loggers
{
    public class NullMooMedLogger : IMainLogger
    {
	    public void Info(string message)
	    {
		    
	    }

	    public void Info(string message, int accountId)
	    {
		    
	    }

	    public void Info(string message, ISessionContext sessionContext)
	    {

	    }

	    public void Debug(string message)
	    {

	    }

	    public void Debug(string message, int accountId)
	    {

	    }

	    public void Debug(string message, ISessionContext sessionContext)
	    {
		    
	    }

	    public void Warning(string message)
	    {

	    }

	    public void Warning(string message, int accountId)
	    {

	    }

	    public void Warning(string message, ISessionContext sessionContext)
        {

        }

	    public void Error(string message)
	    {

	    }

	    public void Error(string message, int accountId)
	    {

	    }

	    public void Error(string message, ISessionContext sessionContext)
		{

		}

	    public void Fatal(string message)
	    {

	    }

	    public void Fatal(string message, int accountId)
	    {

	    }

	    public void Fatal(string message, ISessionContext sessionContext)
		{

		}
	}
}
