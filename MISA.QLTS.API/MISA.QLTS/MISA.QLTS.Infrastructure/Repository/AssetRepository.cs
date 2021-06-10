using Dapper;
using MISA.QLTS.Core.Entities;
using MISA.QLTS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Infrastructure.Repository
{
    public class AssetRepository:BaseRepository<Asset>, IAssetRepository
    {
        public bool CheckStoreCode(Guid? assetId, string assetCode, string functionName)
           
        {
            //var sqlCheckExistCode = "";
            //if (functionName == "Insert")
            //{
            //    // Thực hiện lấy số lượng bản ghi có mã khách hàng trùng với mã khách hàng truyền vào
            //    sqlCheckExistCode = $"Select AssetCode from Asset where Asset.AssetCode = '{assetCode}'";
            //}
            //else if (functionName == "Update")
            //{
            //    // Thực hiện lấy số lượng bản ghi có mã khách hàng trùng với mã khách hàng truyền vào, loại bỏ bản ghi hiện tại
            //    sqlCheckExistCode = $"Select AssetCode from Asset where Asset.AssetCode = '{assetCode}' and  Asset.AssetId != '{assetId}'";
            //}

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("assetId", assetId);
            parameters.Add("assetCode", assetCode);
            parameters.Add("functionName", functionName);
            // thực hiện truy vấn 
            var storeExistCode = _dbConnection
                .Query<string>(
                    $"Proc_DuplicateAsset",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

            // kiểm tra kết quả:
            if (storeExistCode.AsList().Count > 0)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<Asset> GetEntitiesFilter(string input, int recordAmount, int pageNumber, string departmentId, string typeId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("input", input);
            parameters.Add("departmentId", departmentId);
            parameters.Add("typeId", typeId);

            var entities = _dbConnection.Query<Asset>($"Proc_FilterAsset",param: parameters, commandType: CommandType.StoredProcedure).OrderByDescending(s => s.CreatedDate).ToList();

            return entities;    
        }
    }
}
