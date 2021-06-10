using MISA.QLTS.Core.Entities;
using MISA.QLTS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.Service
{
    public class DepartmentService:BaseService<Department>, IDepartmentService
    {
        IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork, IBaseRepository<Department> baseRepository) : base(baseRepository)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
