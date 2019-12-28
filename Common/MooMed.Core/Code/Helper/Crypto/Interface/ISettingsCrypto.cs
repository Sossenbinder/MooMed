using JetBrains.Annotations;

namespace MooMed.Core.Code.Helper.Crypto.Interface
{
    public interface ISettingsCrypto
    {
        [CanBeNull]
        string EncryptSetting([NotNull] string setting, [NotNull] string parameterToEncrypt);

        [CanBeNull]
        string DecryptSetting([NotNull] string setting, [CanBeNull] string parameterToDecrypt);
    }
}
