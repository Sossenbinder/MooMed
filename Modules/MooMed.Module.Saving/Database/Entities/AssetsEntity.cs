using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Saving.Database.Entities
{
	[Table("Assets")]
	public class AssetsEntity : IEntity<int>
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey(nameof(Id))]
		public AccountEntity? Account
		{
			get;
			[UsedImplicitly]
			private set;
		}

		[Column("Cash")]
		public int Cash { get; set; }

		[Column("Debt")]
		public int Debt { get; set; }

		[Column("Equity")]
		public int Equity { get; set; }

		[Column("Estate")]
		public int Estate { get; set; }

		[Column("Commodities")]
		public int Commodities { get; set; }
	}
}