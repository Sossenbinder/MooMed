using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Extensions
{
	public static class TypeExtensions
	{
		public static Type CheckAndGetTaskWrappedType([NotNull] this Type type) 
			=> type.GetGenericTypeDefinition() == typeof(Task<>) ? type.GetGenericArguments()[0] : type;
	}
}
