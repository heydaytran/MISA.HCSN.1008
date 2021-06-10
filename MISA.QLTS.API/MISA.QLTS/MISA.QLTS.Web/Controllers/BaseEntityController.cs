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

        // GET: api/<BaseController>
        [HttpGet]
        public IActionResult Get()
        {
            int a = 0;
            Console.WriteLine(a);
            var responseResult = _baseService.GetEntities();
            return Ok(responseResult);
        }


        // GET api/<BaseController>/5
        [HttpGet("{entityId}")]
        public IActionResult Get(Guid entityId)
        {
            var responseResult = _baseService.GetById(entityId);

            return Ok(responseResult);
        }

        //
        // api/<BaseController>
        [HttpPost]
        public IActionResult Post([FromBody] T entity)
        {
            var responseResult = _baseService.Insert(entity);

            return Ok(responseResult);
        }

        // PUT api/<BaseController>/5
        [HttpPut]
        public IActionResult Put( [FromBody] T entity)
        {
            var responseResult = _baseService.Update(entity);
            return Ok(responseResult);
        }

        // DELETE api/<BaseController>/5
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            var responseResult = _baseService.Delete(entityId);
            return Ok(responseResult);
        }
    }

}
