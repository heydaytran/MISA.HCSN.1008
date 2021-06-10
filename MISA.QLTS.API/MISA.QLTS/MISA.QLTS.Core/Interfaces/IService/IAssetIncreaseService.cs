using MISA.QLTS.Core.Entities;
using MISA.QLTS.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.Interfaces.IService
{
    public interface IAssetIncreaseService:IBaseService<AssetIncrease>
    {
        /// <summary>
        /// Filter và phân trang dữ liệu trả về
        /// </summary>
        /// <param name="input">Dữ liệu tìm kiếm nhập tại thanh tìm kiếm</param>
        /// <param name="recordAmount"> Số lượng bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Trang thứ bao nhiêu</param>
        /// <returns></returns>
        /// author: TVTHINH 1/6/2021
        ResponseResult GetEntitiesFilter(string input, int recordAmount, int pageNumber);
    }
}
