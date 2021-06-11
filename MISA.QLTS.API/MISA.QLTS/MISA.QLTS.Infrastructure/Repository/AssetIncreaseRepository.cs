using Dapper;
using MISA.QLTS.Core.Entities;
using MISA.QLTS.Core.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Infrastructure.Repository
{
    public class AssetIncreaseRepository : BaseRepository<AssetIncrease>, IAssetIncreaseRepository
    {
        /// <summary>
        /// Check trùng mã chứng từ ghi tăng tài sản
        /// </summary>
        /// <param name="assetIncreaseId"> id</param>
        /// <param name="assetIncreaseCode">Mã tài sản ghi tăng</param>
        /// <param name="functionName">update: sửa bản ghi hoặc insert: thêm bản ghi</param>
        /// <returns></returns>
        public bool CheckAssetIcreaseCode(Guid? assetIncreaseId, string assetIncreaseCode, string functionName)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("AssetIncreaseId", assetIncreaseId);
            parameters.Add("ExhibitCode", assetIncreaseCode);
            parameters.Add("functionName", functionName);
            // thực hiện truy vấn 
            var storeExistCode = _dbConnection
                .Query<string>(
                    $"Proc_DuplicateAssetIncrease",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

            // kiểm tra kết quả:
            if (storeExistCode.AsList().Count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// TODO Filter dữ liệu và phân trang( phân trang chức năng tạm bỏ qua)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="recordAmount"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public IEnumerable<AssetIncrease> GetEntitiesFilter(string input, int recordAmount, int pageNumber)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("input", input);

            var entities = _dbConnection.Query<AssetIncrease>($"Proc_FilterAssetIncrease", param: parameters, commandType: CommandType.StoredProcedure).OrderByDescending(s => s.CreatedDate).ToList();

            return entities;
        }
    }
}
