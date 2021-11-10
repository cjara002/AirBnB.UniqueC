using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirBnBUnique.Interfaces;
using AirBnBUnique.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirBnBUnique.Controllers.API
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileAPIController : ControllerBase
    {
        private readonly IProfile _profile;

        public ProfileAPIController(IProfile profile)
        {
            _profile = profile;
        }

        //GET, do after profileService

        //[HttpGet("{id:int}")]
        //[HttpGet()]

        public ActionResult<List<Profile>> Get()
        {
            int iCode = 200;

            List<Profile> list = null;
            try
            {
                list = _profile.GetAll();

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

    }
}