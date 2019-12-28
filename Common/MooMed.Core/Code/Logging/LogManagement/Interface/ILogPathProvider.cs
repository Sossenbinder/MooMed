using JetBrains.Annotations;

namespace MooMed.Core.Code.Logging.LogManagement.Interface
{
    public interface ILogPathProvider
    {
        [NotNull]
        string GetMainLogDirectory();

        [NotNull]
        string GetMainLogPath();

        [NotNull]
        string GetBurialLogDirectory();

        [NotNull]
        string GetTimeStampedBurialPath();
    }
}
