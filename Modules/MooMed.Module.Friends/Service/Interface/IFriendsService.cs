﻿using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Module.Friends.Service.Interface
{
	public interface IFriendsService
	{
		Task<bool> AddFriend([NotNull] ISessionContext sessionContext, int accountId);
	}
}