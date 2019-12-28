using JetBrains.Annotations;

namespace MooMed.Core.Code.Configuration.Interface
{
    public interface IConfigSettingsAccessor
    {
        [CanBeNull]
        string GetValueFromConfigSource([NotNull] string key);
    }
}
