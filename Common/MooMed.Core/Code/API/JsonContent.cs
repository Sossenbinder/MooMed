using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MooMed.Core.Code.API
{
	public class JsonContent : StringContent
	{
		public JsonContent(string content) : base(content)
		{
		}

		public JsonContent(string content, Encoding encoding) : base(content, encoding)
		{
		}

		public JsonContent(string content, Encoding encoding, string mediaType) : base(content, encoding, mediaType)
		{
		}
	}
}
