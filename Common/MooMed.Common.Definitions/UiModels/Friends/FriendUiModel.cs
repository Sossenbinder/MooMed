using System;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Common.Definitions.UiModels.Friends
{
    public class FriendUiModel : IUiModel
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime LastAccessedAt { get; set; }

        public string ProfilePicturePath { get; set; } = null!;

        public AccountOnlineState OnlineState { get; set; }
    }
}