using Microsoft.AspNetCore.Mvc;
using MISA.QLTS.Core.Entities;
using MISA.QLTS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.QLTS.Web.Controllers
{
    public class AssetController : BaseEntityController<Asset>
    {
        private IAssetService _assetService;
        public AssetController(IAssetService assetService) : base(assetService)
        {
            _assetService = assetService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="filterparams"></param>
        /// <param name="departmentId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        [HttpGet("Filter")]
        public IActionResult Get(string input, [FromQuery] FilterStoreParams filterparams, string departmentId, string typeId)
        {
            int a = 0;
            Console.WriteLine(a);
            var responseResult = _assetService.GetEntitiesFilter(input,
                filterparams.recordAmount,
                filterparams.pageNumber,
                departmentId,
                typeId
                );
            return Ok(responseResult);
        }
    }
}
