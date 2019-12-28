using JetBrains.Annotations;

namespace MooMed.Core.Code.Configuration.Interface
{
    public interface IConfigSettingsProvider
    {
        [CanBeNull]
        T ReadValueOrDefault<T>([NotNull] string key);

        [NotNull]
        T ReadValueOrFail<T>([NotNull] string key);

        [CanBeNull]
        T ReadDecryptedValueOrDefault<T>([NotNull] string key, [CanBeNull] string parameterToDecrypt = null);

        [NotNull]
        T ReadDecryptedValueOrFail<T>([NotNull] string key, [CanBeNull] string parameterToDecrypt = null);
    }
}
