using System;
using System.Threading.Tasks;

namespace MooMed.DotNet.Extensions
{
	public static class TypeExtensions
	{
		public static Type CheckAndGetTaskWrappedType(this Type type) 
			=> type.GetGenericTypeDefinition() == typeof(Task<>) ? type.GetGenericArguments()[0] : type;
	}
}
