﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Module.Accounts.Datatypes.Entity
{
    [Table("Account")]
    public class AccountEntity : IEntity<int>
    {
        public int Id { get; set; }

        [NotNull]
        [Column("UserName")]
        public string UserName { get; set; }

        [NotNull]
        [Column("Email")]
        public string Email { get; set; }

        [Column("EmailValidated")]
        public bool EmailValidated { get; set; }

        [NotNull]
        [Column("PasswordHash")]
        public string PasswordHash { get; set; }

        [Column("LastAccessedAt")]
        public DateTime LastAccessedAt { get; set; }

        [CanBeNull]
        [InverseProperty("Account")]
        public AccountOnlineStateEntity AccountOnlineStateEntity { get; set; }

        [NotNull]
        public List<FriendsMappingEntity> FriendsTo { get; set; }

        [NotNull]
        public List<FriendsMappingEntity> FriendsFrom { get; set; }

        [UsedImplicitly]
        // ReSharper disable once NotNullMemberIsNotInitialized
        public AccountEntity()
        {
        }

        public Account ToModel()
        {
	        return new Account
	        {
		        Id = Id,
		        Email = Email,
		        UserName = UserName,
		        EmailValidated = EmailValidated,
		        LastAccessedAt = LastAccessedAt
	        };
        }
	}
}
