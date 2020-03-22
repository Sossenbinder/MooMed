using System;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.User;
using MooMed.Module.Accounts.Datatypes.Entity;

namespace MooMed.Module.Accounts.Repository.Converters
{
	public class FriendsMappingDbConverter : IModelConverter<Friend, AccountEntity>
	{
		public Friend ToModel(AccountEntity entity)
		{
			return new Friend()
			{
				Email = entity.Email,
				Id = entity.Id,
				LastAccessedAt = entity.LastAccessedAt,
				UserName = entity.UserName,
			};
		}
	}
}
