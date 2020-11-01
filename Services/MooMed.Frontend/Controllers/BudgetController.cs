using JetBrains.Annotations;
using MooMed.Frontend.Controllers.Base;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.Frontend.Controllers
{
	public class BudgetController : SessionBaseController
	{
		public BudgetController([NotNull] ISessionService sessionService)
			: base(sessionService)
		{
		}
	}
}