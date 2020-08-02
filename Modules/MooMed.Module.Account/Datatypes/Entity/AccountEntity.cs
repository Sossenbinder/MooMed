using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Module.Accounts.Datatypes.Entity
{
    [Table("Account")]
    public class AccountEntity : IdentityUser<int>, IEntity<int>
    {
	    [Column("LastAccessedAt")]
        public DateTime LastAccessedAt { get; set; }

        // Relations
        [CanBeNull]
        [InverseProperty("Account")]
        public AccountOnlineStateEntity AccountOnlineStateEntity { get; set; }

        [NotNull]
        [InverseProperty("Account")]
        public List<FriendsMappingEntity> FriendsTo { get; set; }

        [NotNull]
        [InverseProperty("Friend")]
        public List<FriendsMappingEntity> FriendsFrom { get; set; }

        [UsedImplicitly]
        // ReSharper disable once NotNullMemberIsNotInitialized
        public AccountEntity()
        {
        }
	}
}
