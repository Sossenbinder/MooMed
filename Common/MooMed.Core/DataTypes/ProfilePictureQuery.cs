using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using ProtoBuf;

namespace MooMed.Core.DataTypes
{
	[ProtoContract]
	public class ProfilePictureQuery : SessionContextAttachedDataType
	{
		[ProtoMember(1)]
		[NotNull]
		public IAsyncEnumerable<byte[]> RawDataStream { get; set; }

		[ProtoMember(2)]
		[NotNull]
		public string FileExtension { get; set; }

		public ProfilePictureQuery([NotNull] ISessionContext sessionContext) 
			: base(sessionContext)
		{
		}
	}
}
