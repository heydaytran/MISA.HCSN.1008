using MISA.QLTS.Core.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.Interfaces
{
    /// <summary>
    /// Interface chứa các việc xử lý nghiệp vụ chung của các đối tượng
    /// CreatedBy: TVThinh 12.05.2021
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Đối tượng Tài sản
        /// </summary>
        IAssetRepository Asset { get; }

        /// <summary>
        /// Đối tượng loại tài sản
        /// </summary>
        IAssetTypeRepository AssetType { get; }

        /// <summary>
        /// Đối tượng phòng ban
        /// </summary>
        IDepartmentRepository Department { get; }

        /// <summary>
        /// Đối tượng ghi ghi tăng tài sản
        /// </summary>
        IAssetIncreaseRepository AssetIncrease { get; }

     
    }
}
