using MISA.QLTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.Interfaces
{
    /// <summary>
    /// interface tài sản
    /// </summary>
    /// author: TVThinh 12.05.2021
    public interface IAssetRepository : IBaseRepository<Asset>
    {
        /// <summary>
        /// Check trùng mã
        /// </summary>
        /// <param name="assetId"> ID</param>
        /// <param name="assetCode"> mã tài sản</param>
        /// <param name="functionName">thao tác sửa hay xóa</param>
        /// <returns></returns>
        public bool CheckStoreCode(Guid? assetId, string assetCode, string functionName);

        /// <summary>
        /// Lấy dữ liệu theo điều kiện( tìm kiếm)
        /// </summary>
        /// <param name="AssetCode">Mã tài sản</param>
        /// <param name="AssetName">Tên tài sản</param>
        /// <returns></returns>
        ///  CreatedBy: TVThinh 12.05.2021
        IEnumerable<Asset> GetEntitiesFilter(string input, int recordAmount, int pageNumber, string departmentId, string typeId);
    }
    
}
