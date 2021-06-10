using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.Entities
{
    public class AssetIncrease:BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        /// author: TVTHINH 30-05-2021

        [Key]
        public Guid? AssetIncreaseId { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        /// author: TVTHINH 30-05-2021

        [Unique]
        [Required]
        public string ExhibitCode { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        /// author: TVTHINH 30-05-2021
        [Required]
        public DateTime ExhibitDate { get; set; }

        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        /// author: TVTHINH 30-05-2021
        [Required]
        public DateTime IncreaseDate { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        /// author: TVTHINH 30-05-2021
        public string Note { get; set; }

        /// <summary>
        /// Chi tiết chứng từ ghi tăng
        /// </summary>
        /// author: TVTHINH 30-05-2021
        [Required]
        public string IncreaseDetail { get; set; }


    }
}
