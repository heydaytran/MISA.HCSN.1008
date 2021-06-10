using Microsoft.AspNetCore.Mvc;
using MISA.QLTS.Core.Entities;
using MISA.QLTS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.QLTS.Web.Controllers
{
    public class AssetTypeController : BaseEntityController<AssetType>
    {

        private IAssetTypeService _assetTypeService;
        public AssetTypeController(IAssetTypeService assetTypeService) : base(assetTypeService)
        {
            _assetTypeService = assetTypeService;
        }

    }
}
