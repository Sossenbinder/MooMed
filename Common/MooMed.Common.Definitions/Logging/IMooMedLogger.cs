using System;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Common.Definitions.Logging
{
    public interface IMooMedLogger
    {
        void Info(string message);

        void Info(string message, int accountId);

        void Info(string message, ISessionContext? sessionContext);

        void Debug(string message);

        void Debug(string message, int accountId);

        void Debug(string message, ISessionContext? sessionContext);

        void Warning(string message);

        void Warning(string message, int accountId);

        void Warning(string message, ISessionContext? sessionContext);

        void Error(string message);

        void Error(string message, int accountId);

        void Error(string message, ISessionContext? sessionContext);

        void Exception(Exception exception);

        void Exception(Exception exception, int accountId);

        void Exception(Exception exception, ISessionContext? sessionContext);

        void Fatal(string message);

        void Fatal(string message, int accountId);

        void Fatal(string message, ISessionContext? sessionContext);
    }
}