using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Extensions
{
	public static class ObjectExtensions
	{
		public static string ToJsonString([NotNull] this object objToConvert)
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(objToConvert);
		}
	}
}
