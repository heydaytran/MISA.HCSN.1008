using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Lớp lưu trữ thông tin chung của các thực thê
/// CreatedBy: TVThinh 15.02.2021
/// </summary>
namespace MISA.QLTS.Core.Entities
{
    ///// <summary>
    ///// Đặc điểm bắt buộc
    ///// </summary>
    //public class Required : Attribute
    //{

    //}
    ///// <summary>
    ///// Đặc điểm duy nhất
    ///// </summary>
    //public class Unique : Attribute
    //{

    //}
    ///// <summary>
    ///// Lớp cơ sở cho các model classes
    ///// </summary>

 
    public class BaseEntity
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày chỉnh sửa cuối
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// Chỉnh sửa lần cuối bởi
        /// </summary>
        public string ModifiedBy { get; set; }
     
    }
}
