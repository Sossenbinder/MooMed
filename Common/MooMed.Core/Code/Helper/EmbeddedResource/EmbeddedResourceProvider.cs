using System;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Helper.EmbeddedResource
{
    public class EmbeddedResourceProvider
    {
        [NotNull]
        private readonly Assembly m_embeddedResourceAssembly;

        public EmbeddedResourceProvider(
            [CanBeNull] Assembly embeddedResourceAssembly = null)
        {
            m_embeddedResourceAssembly = embeddedResourceAssembly != null 
                ? embeddedResourceAssembly 
                : Assembly.GetCallingAssembly();
        }

        [NotNull]
        public Stream GetEmbeddedResourceStream([NotNull] string nameSpace, [NotNull] string name)
        {
            return GetEmbeddedResourceStream($"{nameSpace}.{name}");
        }

        [NotNull]
        private Stream GetEmbeddedResourceStream([NotNull] string completeName)
        {
            if (!DoesAssemblyProvideRequestedEmbeddedResource(completeName))
            {
                throw new ArgumentException("Requested file not found in given assembly");
            }

            return m_embeddedResourceAssembly.GetManifestResourceStream(completeName);
        }

        private bool DoesAssemblyProvideRequestedEmbeddedResource([NotNull] string name)
        {
            var resource = m_embeddedResourceAssembly.GetManifestResourceInfo(name);

            return resource != null;
        }
    }
}
