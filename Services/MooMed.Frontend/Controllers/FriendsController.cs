using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.UiModels.Friends;
using MooMed.Frontend.Controllers.Base;
using MooMed.Frontend.Controllers.Result;
using MooMed.Module.Accounts.Converters;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Frontend.Controllers
{
    public class FriendsController : SessionBaseController
    {
        [NotNull]
        private readonly IAccountService _accountService;

        [NotNull]
        private readonly FriendModelToUiModelConverter _friendModelToUiModelConverter;

        public FriendsController(
            [NotNull] ISessionService sessionService,
            [NotNull] IAccountService accountService,
            [NotNull] FriendModelToUiModelConverter friendModelToUiModelConverter)
            : base(sessionService)
        {
            _accountService = accountService;
            _friendModelToUiModelConverter = friendModelToUiModelConverter;
        }

        [ItemNotNull]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<JsonDataResponse<List<FriendUiModel>>> GetFriends()
        {
            var getFriendsResponse = await _accountService.GetFriends(CurrentSessionOrFail);

            return getFriendsResponse.ToUiModelJsonResponse(_friendModelToUiModelConverter);
        }

        [ItemNotNull]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddFriend([NotNull][FromBody] AddFriendUiModel addFriendUiModel)
        {
            var message = new AddAsFriendModel()
            {
                SessionContext = CurrentSessionOrFail,
                AccountId = addFriendUiModel.AccountId,
            };

            var friendAddResponse = await _accountService.AddAsFriend(message);

            return friendAddResponse.ToJsonResponse();
        }
    }
}