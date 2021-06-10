using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.QLTS.Core.Interfaces;
using MISA.QLTS.Core.Result;

using MISA.QLTS.Core.Resource;

namespace MISA.QLTS.Core.Service
{
    public class BaseService<T> : IBaseService<T>
    {
        protected IBaseRepository<T> _baseRespository;

        public BaseService(IBaseRepository<T> baseRespository)
        {
            _baseRespository = baseRespository;
        }

        public ResponseResult GetById(Guid entityId)
        {
            var result = new ResponseResult();
            if (string.IsNullOrEmpty(entityId.ToString()))
            {
                result.IsSuccess = false;
                result.ErrorCode = Enum.ErrorCode.BADREQUEST;
                result.DevMsg = ResourceMessage.Error_NotExist;
            }
            else
            {
                var entity = _baseRespository.GetById(entityId);
                if (entity != null)
                {
                    result.Data = entity;
                    result.IsSuccess = true;
                    result.ErrorCode = Enum.ErrorCode.NONE;
                    result.UserMsg = ResourceMessage.Get_Success;

                }
                else
                {
                    result.IsSuccess = false;
                    result.DevMsg = ResourceMessage.Error_Store_NotExist;
                    result.UserMsg = ResourceMessage.Error_Store_NotExist;
                }
            }

            return result;
        }

        public ResponseResult GetEntities()
        {
            var entities = _baseRespository.GetEntities();

            var result = new ResponseResult();
            if (entities != null)
            {
                result.Data = entities;
                result.ErrorCode = Enum.ErrorCode.NONE;
                result.UserMsg = ResourceMessage.Get_Success;
            }
            else
            {
                result.IsSuccess = false;
                result.UserMsg = ResourceMessage.NotFound;
                result.DevMsg = ResourceMessage.NoContent;
                result.ErrorCode = Enum.ErrorCode.NOCONTENT;
            }

            return result;
        }

        public ResponseResult Insert(T entity)
        {
            var result = new ResponseResult();
            string functionName = "Insert";

            // Validate nghiệp vụ
            Validate(result, entity, null, functionName);

            if (result.IsSuccess == true)
            {
                // Thực hiện thêm mới
                var rowAffects = _baseRespository.Insert(entity);

                if (rowAffects == 1)
                {
                    result.IsSuccess = true;
                    result.ErrorCode = Enum.ErrorCode.NONE;
                    result.UserMsg = ResourceMessage.Insert_Success;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorCode = Enum.ErrorCode.EXCEPTION;
                    result.DevMsg = ResourceMessage.Error_Insert;
                    result.UserMsg = ResourceMessage.Error_Insert;
                }
            }

            return result;
        }

        public ResponseResult Update(T entity)
        {

            var result = new ResponseResult();

            string functionName = "Update";

            //Validate nghiệp vụ khi cập nhật

            var IdPropetyName = entity.GetType().Name + "Id";
            var IdPropetyValue = typeof(T).GetProperty(IdPropetyName).GetValue(entity);

            Validate(result, entity, (Guid)IdPropetyValue, functionName);

            if (result.IsSuccess == true)
            {
                // thực hiện cập nhật
                var rowAffects = _baseRespository.Update(entity);

                if (rowAffects == -1)
                {
                    result.IsSuccess = false;
                    result.ErrorCode = Enum.ErrorCode.EXCEPTION;
                    result.DevMsg = ResourceMessage.Error_Update;
                    result.UserMsg = ResourceMessage.Error_Update;
                }
                else
                {
                    result.IsSuccess = true;
                    result.ErrorCode = Enum.ErrorCode.NONE;
                    result.UserMsg = ResourceMessage.Update_Success;
                }
            }
            return result;
        }

        // xóa một bản ghi
        public ResponseResult Delete(Guid entityId)
        {
            // thực hiện cập nhật
            var rowAffects = _baseRespository.Delete(entityId);

            var result = new ResponseResult();
            if (rowAffects == 1)
            {
                result.ErrorCode = Enum.ErrorCode.NONE;
                result.IsSuccess = true;
            }

            else
            {
                result.IsSuccess = false;
                result.UserMsg = ResourceMessage.NoContent;
                result.DevMsg = ResourceMessage.NoContent;
                result.ErrorCode = Enum.ErrorCode.NOCONTENT;
            }

            return result;
        }

        /// <summary>
        /// Hàm validate dữ liệu 
        /// </summary>
        /// <param name="responseResult">Kết quả trả về xem có </param>
        /// <param name="entity"></param>
        public virtual void Validate(ResponseResult responseResult, T entity, Guid? entityID, string functionName)
        {

        }

        /// <summary>
        ///Hàm validate dữ liệu khi thực hiện thêm bản ghỉ
        ///Cho phép kế thừa để thực hiện nghiệp vụ với đối tượng tương ứng
        /// </summary>
        /// <param name="entity">Thực thể cần thêm</param>
        public virtual void ValidateInsert(T entity)
        {

        }

        /// <summary>
        /// Hàm validate dữ liệu khi thực hiện sửa thông tin bản ghi
        /// Cho phép kế thừa để thực hiện nghiệp vụ với đối tượng tương ứng
        /// </summary>
        /// <param name="entity">Thực thể cần sửa</param>
        /// <param name="entiyId">ID của thực thể cần sửa</param>
        public virtual void ValidateUpdate(T entity, Guid entiyId)
        {

        }

    }
}
