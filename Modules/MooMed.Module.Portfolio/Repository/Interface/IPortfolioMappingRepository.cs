using System;
using System.Collections.Generic;
using System.Text;
using MooMed.Common.Database.Repository.Interface;
using MooMed.Module.Portfolio.DataTypes.Entity;

namespace MooMed.Module.Portfolio.Repository.Interface
{
	public interface IPortfolioMappingRepository : ICrudRepository<PortfolioMappingEntity, int>
	{
	}
}
