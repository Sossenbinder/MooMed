using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using JetBrains.Annotations;
using MooMed.Core.Code.Helper.EmbeddedResource;

namespace MooMed.Core.Code.Helper.Config
{
    public class ConfigReader
    {
        [NotNull]
        private readonly EmbeddedResourceProvider _embeddedResourceProvider;

        public ConfigReader()
        {
            _embeddedResourceProvider = new EmbeddedResourceProvider();
        }

        /// <summary>
        /// Sets path of configuration file to be read
        /// </summary>
        /// <param name="name">Name of configuration file</param>
        /// <returns>Bool reflecting whether </returns>
        [NotNull]
        public async Task<Dictionary<string, string>> LoadConfigPath([NotNull] string name)
        {
            return await LoadConfigFileFromEmbeddedResources(name);
        }

        [NotNull]
        private async Task<Dictionary<string, string>> LoadConfigFileFromEmbeddedResources([NotNull] string name)
        {
            var embeddedStream = _embeddedResourceProvider.GetEmbeddedResourceStream("MooMed.Core.Configuration", name);

            return await ParseConfigFile(embeddedStream);
        }

        [ItemNotNull]
        [NotNull]
        private static async Task<Dictionary<string, string>> ParseConfigFile([NotNull] Stream embeddedStream)
        {
            var keyValueDict = new Dictionary<string, string>();

            using (var xmlReader = XmlReader.Create(embeddedStream, new XmlReaderSettings
                {
                    Async = true
                })
            )
            {
                await xmlReader.MoveToContentAsync();

                while (await xmlReader.ReadAsync())
                {
                    if (xmlReader.NodeType != XmlNodeType.Element || xmlReader.Name != "setting")
                    {
                        continue;
                    }

                    var key = xmlReader.GetAttribute("key");
                    var value = xmlReader.GetAttribute("value");

                    if (key != null && value != null)
                    {
                        keyValueDict.Add(key, value);
                    }
                }
            }

            return keyValueDict;
        }
    }
}
