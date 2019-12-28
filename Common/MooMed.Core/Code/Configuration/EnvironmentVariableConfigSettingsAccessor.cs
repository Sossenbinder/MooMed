using System;
using MooMed.Core.Code.Configuration.Interface;

namespace MooMed.Core.Code.Configuration
{
    public class EnvironmentVariableConfigSettingsAccessor : IConfigSettingsAccessor
    {
        public string GetValueFromConfigSource(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }
    }
}
