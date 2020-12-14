using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Module.Saving.Service.Interface
{
    public interface IAssetService
    {
        Task SetAssets(ISessionContext sessionContext, AssetsModel assets);
    }
}