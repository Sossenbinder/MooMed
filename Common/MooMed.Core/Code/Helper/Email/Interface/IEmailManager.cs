using System.Collections.Generic;
using System.Threading.Tasks;

namespace MooMed.Core.Code.Helper.Email.Interface
{
    public interface IEmailManager
    {
        Task Send(string recipient, string subject, string messageContent);

        Task Send(IEnumerable<string> recipients, string subject, string messageContent);
    }
}
