using System;
using System.Runtime.Serialization;
using MooMed.Common.Definitions.Interface;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Common.Definitions.UiModels.Friends
{
    public class FriendUiModel : IUiModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime LastAccessedAt { get; set; }

        public string ProfilePicturePath { get; set; }

        public AccountOnlineState OnlineState { get; set; }
    }
}