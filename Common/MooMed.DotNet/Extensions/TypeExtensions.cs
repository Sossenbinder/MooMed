using System;
using System.Threading.Tasks;

namespace MooMed.DotNet.Extensions
{
	public static class TypeExtensions
	{
		/// <summary>
		/// Accepts a type and returns it stripped from a Task<> wrap if available
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static Type CheckAndGetTaskWrappedType(this Type type)
			=> type.GetGenericTypeDefinition() == typeof(Task<>) ? type.GetGenericArguments()[0] : type;
	}
}