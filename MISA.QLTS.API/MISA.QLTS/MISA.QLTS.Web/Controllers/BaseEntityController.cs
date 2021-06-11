using Microsoft.AspNetCore.Mvc;
using MISA.QLTS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.QLTS.Web.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class BaseEntityController<T> : ControllerBase
    {
        private IBaseService<T> _baseService;

        public BaseEntityController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// TODO Lấy toàn bộ dữ liệu của đối tượng
        /// </summary>
        /// <returns></returns>
        // GET: api/<BaseController>
        [HttpGet]
        public IActionResult Get()
        {
            int a = 0;
            Console.WriteLine(a);
            var responseResult = _baseService.GetEntities();
            return Ok(responseResult);
        }

        /// <summary>
        /// TODO lấy dữ liệu theo ID
        /// </summary>
        /// <param name="entityId">ID</param>
        /// <returns></returns>
        // GET api/<BaseController>/5
        [HttpGet("{entityId}")]
        public IActionResult Get(Guid entityId)
        {
            var responseResult = _baseService.GetById(entityId);

            return Ok(responseResult);
        }

        /// <summary>
        /// TODO Insert 1 bản ghi 
        /// </summary>
        /// <param name="entity">Bản ghi được insert</param>
        /// <returns></returns>
        // api/<BaseController>
        [HttpPost]
        public IActionResult Post([FromBody] T entity)
        {
            var responseResult = _baseService.Insert(entity);

            return Ok(responseResult);
        }

        /// <summary>
        /// TODO Update 1 bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi được update</param>
        /// <returns></returns>
        // PUT api/<BaseController>/5
        [HttpPut]
        public IActionResult Put( [FromBody] T entity)
        {
            var responseResult = _baseService.Update(entity);
            return Ok(responseResult);
        }

        /// <summary>
        /// TODO Xóa 1 bản ghi theo ID
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        // DELETE api/<BaseController>/5
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            var responseResult = _baseService.Delete(entityId);
            return Ok(responseResult);
        }
    }

}
