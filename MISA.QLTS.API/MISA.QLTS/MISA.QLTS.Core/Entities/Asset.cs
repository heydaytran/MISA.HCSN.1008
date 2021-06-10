using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.Entities
{
    /// <summary>
    /// Thông tin tài sản
    /// Author: TVThinh 15.02.2021
    /// </summary>
    public class Asset:BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        
        [Key]
        public Guid? AssetId { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>

        [Unique]
        [Required]
        public string AssetCode { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        
        [Required]
        public string AssetName { get; set; }
        /// <summary>
        /// Loại tài sản
        /// </summary>
        public Guid? AssetTypeId { get; set; }

        /// <summary>
        /// Phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        public DateTime? IncreaseDate { get; set; }
        /// <summary>
        /// Thời gian sử dụng
        /// </summary>
        public double? TimeUse { get; set; }
        /// <summary>
        /// Tỉ lệ hao mòn
        /// </summary>
        public double? WearRate { get; set; }
        /// <summary>
        /// Giá gốc
        /// </summary>
        public double? OriginalPrice { get; set; }
        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        public double? WearValue { get; set; }
        /// <summary>
        /// Sử dụng  (1- sử dụng; 0 - không sử dụng)
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string AssetTypeName { get; set; }
    }
}
