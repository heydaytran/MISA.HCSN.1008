using MISA.QLTS.Core.Entities;
using MISA.QLTS.Core.Interfaces;
using MISA.QLTS.Core.Resource;
using MISA.QLTS.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.Service
{
    public class AssetService : BaseService<Asset>, IAssetService
    {
        IUnitOfWork _unitOfWork;
        public AssetService(IUnitOfWork unitOfWork, IBaseRepository<Asset> baseRepository) : base(baseRepository)
        {
            _unitOfWork = unitOfWork;
        }


        // TODO filter dữ liệu và phân trang
        public ResponseResult GetEntitiesFilter(string input, int recordAmount, int pageNumber, string departmentId, string typeId)
        {
            var entities = _unitOfWork.Asset.GetEntitiesFilter(input, recordAmount, pageNumber, departmentId, typeId).ToList();

            var result = new ResponseResult();
            List<Asset> b = new List<Asset>();

            // tính ra tổng số trang dựa trên tổng số bản ghi
            int totlaPage = 1;
            if (entities.Count % recordAmount != 0)
            {

                totlaPage = entities.Count / recordAmount + 1;
            }
            else
            {
                //
                totlaPage = entities.Count / recordAmount;
            }

            //
            int limit;
            if (recordAmount * pageNumber > entities.Count)
            {
                //
                limit = entities.Count;

            }
            else
            {
                limit = recordAmount * pageNumber;
            }

            //
            for (int i = recordAmount * pageNumber - recordAmount; i < limit; i++)
            {
                b.Add(entities[i]);
            }

            //
            if (b != null)
            {
                result.Data = b;
                result.IsSuccess = true;
                result.ErrorCode = Enum.ErrorCode.NONE;
                result.UserMsg = Resource.ResourceMessage.Get_Success;
                result.TotalPage = totlaPage;
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorCode = Enum.ErrorCode.NOCONTENT;
                result.UserMsg = Resource.ResourceMessage.Error_Filter;
                result.DevMsg = Resource.ResourceMessage.Error_Filter;
            }

            return result;

        }

        // TODO validate dữ liệu nhập từ client
        public override void Validate(ResponseResult responseResult, Asset entity, Guid? entityID, string functionName)
        {
            var propertyUnique = "Mã tài sản";
            var propertyRequired = new Dictionary<string, string>();
            propertyRequired.Add("Mã tài sản", entity.AssetCode);
            propertyRequired.Add("Tên tài sản", entity.AssetName);

            foreach (var property in propertyRequired)
            {
                if (string.IsNullOrEmpty(property.Value))
                {
                    responseResult.IsSuccess = false;
                    responseResult.ErrorCode = Enum.ErrorCode.BADREQUEST;
                    responseResult.DevMsg = property.Key + " " + Resource.ResourceMessage.Error_Required;
                    responseResult.UserMsg = property.Key + " " + Resource.ResourceMessage.Error_Required;
                }
            }

            // kiểm tra xem trường nào là duy nhất (có thuộc tính Unique) thì check duplicate

            bool checkDuplicateCode = _unitOfWork.Asset.CheckStoreCode(entityID, entity.AssetCode, functionName);

            if (checkDuplicateCode)
            {
                responseResult.IsSuccess = false;
                responseResult.ErrorCode = Enum.ErrorCode.BADREQUEST;
                responseResult.DevMsg = propertyUnique + " " + Resource.ResourceMessage.Error_Duplicate;
                responseResult.UserMsg = propertyUnique + " " + Resource.ResourceMessage.Error_Duplicate;
            }
        }

    }
}
