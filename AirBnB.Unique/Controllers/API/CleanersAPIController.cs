using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirBnB.Unique.Interfaces;
using AirBnB.Unique.Models.Domain;
using AirBnB.Unique.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirBnB.Unique.Controllers.API
{
    [Route("api/cleaners")]
    [ApiController]
    public class CleanersAPIController : ControllerBase
    {
        private readonly ICleaners _cleaners;

        public CleanersAPIController(ICleaners cleaners)
        {
            _cleaners = cleaners;
        }

        public string Index(int id)
        {
            return "I got " + id.ToString();
        }

        [HttpGet]
        public ActionResult<List<Cleaners>> Get()
        {
            int iCode = 200;

            List<Cleaners> list = null;
            try
            {
                list = _cleaners.GetAll();

                if (list == null)
                {
                    iCode = 404;

                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                iCode = 500;
                throw ex;
            }
            return StatusCode(iCode, list);
        }

        [HttpGet("paginate")]

        public ActionResult<List<Cleaners>> Paginate(int pageIndex, int pageSize)
        {
            int iCode = 200;
            Paged<Cleaners> paged = null;
            
            try
            {
                paged = _cleaners.Paginate(pageIndex, pageSize);
                if (paged == null)
                {
                    iCode = 404;
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                iCode = 500;
                throw ex;
            }
            return StatusCode(iCode, paged);
        }

        [HttpGet("search")]

        public ActionResult<List<Cleaners>> SearchPaginate(int pageIndex, int pageSize, string query)
        {
            int iCode = 200;
            Paged<Cleaners> paged = null;

            try
            {
                paged = _cleaners.SearchPaginate(pageIndex, pageSize, query);
                if (paged == null)
                {
                    iCode = 404;
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                throw ex;
            }
            return StatusCode(iCode, paged);
        }

        [HttpGet("{Id:int}")]
        public ActionResult<List<Cleaners>> GetById(int Id)
        {
            int iCode = 200;

            List<Cleaners> list = null;
            try
            {
                list = _cleaners.GetById(Id);

                if (list == null)
                {
                    iCode = 404;

                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                throw ex;
            }
            return StatusCode(iCode, list);
        }

        [HttpPost]

        public ActionResult<Cleaners> Create(CleanersAddRequest model)
        {
            ObjectResult result = null;

            try
            {
                int Id = _cleaners.Add(model);

                result = Created("success", "id:" + Id);
            }
            catch (Exception ex)
            {
                result = StatusCode(500, ex.Message.ToString());
            }

            return result;
        }


        [HttpPut("{id:int}")]

        public ActionResult<Cleaners> Update(CleanersUpdateRequest model, int id)
        {
        //int iCode = 200;

         ObjectResult result = null;

         try
         {
          _cleaners.Update(model, id);
                result = Ok(200);
           
         }

         catch (Exception ex)
        {
            result = StatusCode(500, ex.Message.ToString());
        }
        return result;
        }

        
        [HttpDelete("{id:int}")]
        public ActionResult<Cleaners> DeleteById(int Id)
        {
            ObjectResult result = null;
            try
            {
                _cleaners.DeleteById(Id);
                result = Ok(200);

            }
            catch(Exception ex)
            {
                result = StatusCode(500, ex.Message.ToString());
                
            }
            return result;
        }
        
    }

   

   
}