using System;
using System.Collections.Generic;
using System.Xml;
using JetBrains.Annotations;
using MooMed.Core.Code.Configuration.Interface;

namespace MooMed.TestBase.Config
{
    public class UnitTestSettingsAccessor : IConfigSettingsAccessor
    {
        private Dictionary<string, string> m_settingsDict;

        public UnitTestSettingsAccessor()
        {
            LoadXml("UnitTestSettings.xml");
        }

        private void LoadXml([NotNull] string xmlPath)
        {
            var keyValueDict = new List<KeyValuePair<string, string>>();

            using (var xmlReader = XmlReader.Create(xmlPath, new XmlReaderSettings()))
            {
                xmlReader.MoveToContent();

                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType != XmlNodeType.Element || !xmlReader.Name.Equals("setting", StringComparison.CurrentCultureIgnoreCase))
                    {
                        continue;
                    }
					
                    var key = xmlReader.GetAttribute("Name");
                    var value = xmlReader.GetAttribute("Value");

                    if (key != null && value != null)
                    {
                        keyValueDict.Add(new KeyValuePair<string, string>(key, value));
                    }
                }
            }

            m_settingsDict = new Dictionary<string, string>(keyValueDict);
        }

        public string GetValueFromConfigSource(string key)
        {
            return m_settingsDict[key];
        }
    }
}
