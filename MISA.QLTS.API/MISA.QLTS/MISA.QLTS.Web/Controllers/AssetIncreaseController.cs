using Microsoft.AspNetCore.Mvc;
using MISA.QLTS.Core.Entities;
using MISA.QLTS.Core.Interfaces;
using MISA.QLTS.Core.Interfaces.IService;
using MISA.QLTS.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.QLTS.Web.Controllers
{
    public class AssetIncreaseController : BaseEntityController<AssetIncrease>
    {
        private IAssetIncreaseService _assetService;
        public AssetIncreaseController(IAssetIncreaseService assetService) : base(assetService)
        {
            _assetService = assetService;
        }

        /// <summary>
        /// filter dữ liệu và phân trang
        /// </summary>
        /// <param name="input"></param>
        /// <param name="filterparams"></param>
        /// <returns></returns>
        [HttpGet("Filter")]
        public IActionResult Get(string input, [FromQuery] FilterStoreParams filterparams)
        {
            int a = 0;
            Console.WriteLine(a);
            var responseResult = _assetService.GetEntitiesFilter(input,
                filterparams.recordAmount,
                filterparams.pageNumber
                );
            return Ok(responseResult);
        }

    }
}
