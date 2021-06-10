using MISA.QLTS.Core.Entities;
using MISA.QLTS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.Service
{
    public class AssetTypeService:BaseService<AssetType>, IAssetTypeService 
    {
        IUnitOfWork _unitOfWork;
        public AssetTypeService(IUnitOfWork unitOfWork, IBaseRepository<AssetType> baseRepository) : base(baseRepository)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
