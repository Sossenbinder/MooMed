using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.UiModels.Friends;

namespace MooMed.Module.Accounts.Converters
{
    public class FriendModelToUiModelConverter : IModelToUiModelConverter<Friend, FriendUiModel>
    {
        public FriendUiModel ToUiModel(Friend friend)
        {
            return new FriendUiModel()
            {
                Email = friend.Email,
                Id = friend.Id,
                LastAccessedAt = friend.LastAccessedAt,
                OnlineState = friend.OnlineState,
                ProfilePicturePath = friend.ProfilePicturePath,
                UserName = friend.UserName,
            };
        }
    }
}