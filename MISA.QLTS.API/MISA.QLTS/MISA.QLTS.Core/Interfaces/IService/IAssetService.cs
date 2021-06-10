using MISA.QLTS.Core.Entities;
using MISA.QLTS.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.Interfaces
{
    public interface IAssetService:IBaseService<Asset>
    {
        /// <summary>
        /// Lấy dữ liệu theo điều kiện( tìm kiếm)
        /// </summary>
        /// <param name="AssetCode">Mã tài sản</param>
        /// <param name="AssetName">Tên tài sản</param>
        /// <returns></returns>
        ///  CreatedBy: TVThinh 12.05.2021
        ResponseResult GetEntitiesFilter(string input, int recordAmount, int pageNumber, string departmentId, string typeId);
    }
}
