using MISA.QLTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.Interfaces.IRepository
{
    public interface IAssetIncreaseRepository:IBaseRepository<AssetIncrease>
    {
        /// <summary>
        /// Check trùng mã 
        /// </summary>
        /// <param name="assetIncreaseId"></param>
        /// <param name="assetIncreaseCode"></param>
        /// <param name="functionName"></param>
        /// <returns>true: trùng, false: không trùng</returns>
        /// author : TVTHINH 1/6/2021
        public bool CheckAssetIcreaseCode(Guid? assetIncreaseId, string assetIncreaseCode, string functionName);

        /// <summary>
        /// Filter và phân trang dữ liệu trả về
        /// </summary>
        /// <param name="input"> Dữ liệu thanh tìm kiếm</param>
        /// <param name="recordAmount"> Tổng số bản ghi trên 1 trang</param>
        /// <param name="pageNumber"> Trang thứ bao nhiêu</param>
        /// <returns></returns>
        /// author: TVTHINH 1/6/2021
        IEnumerable<AssetIncrease> GetEntitiesFilter(string input, int recordAmount, int pageNumber);
    }
}
