using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Module.Saving.Service.Interface;

namespace MooMed.Module.Saving.Service
{
    public class AssetService : IAssetService
    {
        public Task SetAssets(ISessionContext sessionContext, AssetsModel assets)
        {
            throw new System.NotImplementedException();
        }
    }
}