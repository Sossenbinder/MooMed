﻿using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Saving.Database.Entities
{
	[Table("CurrencyMapping")]
	public class CurrencyMappingEntity : IEntity<int>
	{
		public int Id { get; set; }

		[ForeignKey("Id")]
		public AccountEntity Account
		{
			get;
			[UsedImplicitly]
			private set;
		}

		[Column("Currency")]
		public Currency Currency { get; set; }
	}
}