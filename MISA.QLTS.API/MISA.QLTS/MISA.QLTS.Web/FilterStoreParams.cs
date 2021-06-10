using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.QLTS.Web
{
    public class FilterStoreParams
    {
        /// <summary>
        /// Số bản ghi trên 1 trang
        /// </summary>
        public int recordAmount { get; set; }

        /// <summary>
        /// Trường để đánh số cho trang
        /// </summary>
        public int pageNumber { get; set; }
    }
}
