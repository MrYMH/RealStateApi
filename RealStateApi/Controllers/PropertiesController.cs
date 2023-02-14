using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealStateApi.Data;
using RealStateApi.Models;
using System.Security.Claims;

namespace RealStateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ApiDbContext _db;
        public PropertiesController(ApiDbContext db)
        {
            _db = db;
        }


        //api/Properties/Add ==
        [HttpPost("[action]")]
        [Authorize]
        public IActionResult Add([FromBody] Property prop)
        {
            //get email claims from jwt
            if (prop == null)
            {
                return NoContent();
            }
            else
            {

                var userEmial = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                // error ==>? userEmail is null;
                var user = _db.User.FirstOrDefault(u => u.Email == userEmial);
                if (user == null)
                {
                    return NotFound();
                }
                //add property record
                prop.IsTrending = true;
                prop.UserId = user.Id;
                _db.Properties.Add(prop);
                //save changes
                _db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);

            }




        }


        //api/Properties/2 ==
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] Property prop)
        {
            var propFromDb = _db.Properties.FirstOrDefault(c => c.Id == id);
            if (propFromDb == null)
            {
                return NoContent();
            }
            else
            {
                var userEmial = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _db.User.FirstOrDefault(u => u.Email == userEmial);
                if (user == null)
                {
                    return NotFound();
                }

                if (propFromDb.UserId == user.Id)
                {
                    //add property record
                    propFromDb.IsTrending = true;
                    propFromDb.UserId = user.Id;
                    //update props
                    propFromDb.Name = prop.Name;
                    propFromDb.Detail = prop.Detail;
                    propFromDb.price = prop.price;
                    propFromDb.Address = prop.Address;

                    //save changes
                    _db.SaveChanges();
                    return Ok("Property Updated ");
                }
                else
                {
                    return BadRequest("wrong user");
                }



            }

        }


        //api/Properties/3 ==
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var propFromDb = _db.Properties.FirstOrDefault(c => c.Id == id);
            if (propFromDb == null)
            {
                return NoContent();
            }
            else
            {
                var userEmial = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _db.User.FirstOrDefault(u => u.Email == userEmial);
                if (user == null)
                {
                    return NotFound();
                }

                if (propFromDb.UserId == user.Id)
                {
                    _db.Properties.Remove(propFromDb);
                    //save changes
                    _db.SaveChanges();
                    return Ok("Property deleted ");
                }
                else
                {
                    return BadRequest("wrong user");
                }

            }

        }


        //api/Properties/PropertiesList?CategoryId=2 ==
        [HttpGet("PropertiesList")]
        [Authorize]
        public IActionResult GetProperties(int categoryId)
        {
            var propertiesList = _db.Properties.Where(c=>c.CategoryId == categoryId);
            if(propertiesList == null) 
            {
                return NotFound();

            }
            else
            {
                return Ok(propertiesList);
            }
        }

        //api/Properties/PropertyDetail?id=2 ==
        [HttpGet("PropertyDetail")]
        [Authorize]
        public IActionResult GetPropertyDetail(int id)
        {
            var property = _db.Properties.FirstOrDefault(p=>p.Id==id);
            if (property == null)
            {
                return NotFound();

            }
            else
            {
                return Ok(property);
            }
        }



        //api/Properties/TrendingProperties ==
        [HttpGet("TrendingProperties")]
        [Authorize]
        public IActionResult GetTrendingProperties()
        {
            var propertiesList = _db.Properties.Where(c => c.IsTrending==true);
            if (propertiesList == null)
            {
                return NotFound();

            }
            else
            {
                return Ok(propertiesList);
            }
        }

        //api/Properties/SearchProperties?address=aswan 
        [HttpGet("SearchProperties")]
        [Authorize]
        public IActionResult GetSearchProperties(string address)
        {
            var propertiesList = _db.Properties.Where(c => c.Address.Contains(address));
            if (propertiesList == null)
            {
                return NotFound();

            }
            else
            {
                return Ok(propertiesList);
            }
        }

    }
}
