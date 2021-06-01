using System.Linq;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Module.Saving.Converters;
using MooMed.Module.Saving.Repository.Interface;
using MooMed.Module.Saving.Service.Interface;

namespace MooMed.Module.Saving.Service
{
	public class AssetService : IAssetService
	{
		private readonly IAssetRepository _assetRepository;

		private readonly AssetEntityConverter _assetEntityConverter;

		public AssetService(
			IAssetRepository assetRepository,
			AssetEntityConverter assetEntityConverter)
		{
			_assetRepository = assetRepository;
			_assetEntityConverter = assetEntityConverter;
		}

		public async Task SetAssets(ISessionContext sessionContext, AssetsModel assets)
		{
			var entity = _assetEntityConverter.ToEntity(assets);

			await _assetRepository.CreateOrUpdate(entity, x => x = entity);
		}

		public async Task<AssetsModel?> GetAssets(ISessionContext sessionContext)
		{
			var assets = await _assetRepository.Read(x => x.Id == sessionContext.Account.Id);

			var asset = assets.FirstOrDefault();

			if (asset == null)
			{
				return null;
			}

			return _assetEntityConverter.ToModel(asset);
		}
	}
}