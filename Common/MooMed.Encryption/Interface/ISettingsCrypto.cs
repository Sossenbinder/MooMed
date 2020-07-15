namespace MooMed.Encryption.Interface
{
    public interface ISettingsCrypto
    {
        string EncryptSetting(string setting, string parameterToEncrypt);

        string DecryptSetting( string setting, string parameterToDecrypt);
    }
}
