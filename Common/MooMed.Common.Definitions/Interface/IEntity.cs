using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace MooMed.Common.Definitions.Interface
{
	public interface IEntity<TKeyType>
	{
		[NotNull]
		[Key]
		[Column("Id")]
		public TKeyType Id { get; set; }
	}
}
