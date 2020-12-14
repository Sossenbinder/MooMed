using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Module.Accounts.Datatypes.Entity
{
    [Table("FriendsMapping")]
    public class FriendsMappingEntity : IEntity<int>
    {
        public int Id { get; set; }

        public object[] GetKeys()
        {
            return new object[]
            {
                Id,
                FriendId,
            };
        }

        [ForeignKey("Id")]
        public AccountEntity Account
        {
            get;
            [UsedImplicitly]
            private set;
        }

        [Key]
        [Column("FriendId")]
        public int FriendId { get; set; }

        [ForeignKey("FriendId")]
        public AccountEntity Friend
        {
            get;
            [UsedImplicitly]
            private set;
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is FriendsMappingEntity comparisonItem))
            {
                return false;
            }

            return Id!.Equals(comparisonItem.Id) && FriendId.Equals(comparisonItem.FriendId);
        }

        public override int GetHashCode()
        {
            var equalityComparer = EqualityComparer<int>.Default;

            return HashCode.Combine(
                equalityComparer.GetHashCode(Id!),
                equalityComparer.GetHashCode(FriendId!));
        }
    }
}