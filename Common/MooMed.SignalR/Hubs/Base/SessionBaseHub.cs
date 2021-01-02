using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.SignalR.Hubs.Base
{
    public class SessionBaseHub : BaseHub
    {
        [NotNull]
        private readonly ISessionService _sessionService;

        protected SessionBaseHub([NotNull] ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        protected async Task<ISessionContext> GetSessionContextOrFail()
        {
            var accountId = Convert.ToInt32(Context.User.Identity.Name);

            var serviceResponse = await _sessionService.GetSessionContext(accountId);

            return serviceResponse.PayloadOrFail;
        }
    }
}