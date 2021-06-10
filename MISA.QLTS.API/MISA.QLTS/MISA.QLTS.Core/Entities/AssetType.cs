using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.Entities
{
    /// <summary>
    /// Thông tin về loại tài sản
    /// Author: TVThinh 15.02.2021
    /// </summary>
    public class AssetType
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid? AssetTypeId { get; set; }
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string AssetTypeCode { get; set; }
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string AssetTypeName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
    }
}
