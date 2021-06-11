using MISA.QLTS.Core.Entities;
using MISA.QLTS.Core.Interfaces;
using MISA.QLTS.Core.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.EShop.Infrastructure.Repository
{
    /// <summary>
    /// TODO xử lý nghiệp vụ chung của các đối tượng
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IAssetRepository asset, 
            IAssetTypeRepository assetType, 
            IDepartmentRepository department,
            IAssetIncreaseRepository assetIncrease
            )
        {
            Asset = asset;
            AssetType = assetType;
            Department = department;
            AssetIncrease = assetIncrease;
        }

        public IAssetRepository Asset { get; }

        public IAssetTypeRepository AssetType { get; }

        public IDepartmentRepository Department { get; }

        public IAssetIncreaseRepository AssetIncrease { get; }
    }
}
