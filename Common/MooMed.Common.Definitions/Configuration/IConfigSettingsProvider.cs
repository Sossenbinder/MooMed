namespace MooMed.Common.Definitions.Configuration
{
    public interface IConfigSettingsProvider
    {
        T ReadValue<T>(string key, T optionalValueIfNotFound = default);

        T ReadValueOrFail<T>(string key);

        T ReadDecryptedValue<T>(string key, string? parameterToDecrypt = null);

        T ReadDecryptedValueOrFail<T>(string key, string? parameterToDecrypt = null);
    }
}
