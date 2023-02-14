using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApi.Data;
using RealStateApi.Models;

namespace RealStateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiDbContext _db;

        public CategoriesController(ApiDbContext db)
        {
            _db = db;   
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Categories);
        }



        //api/Categories/Add
        [HttpPost("[action]")]
        [Authorize]
        public IActionResult Add([FromBody] Category obj)
        {
            if (obj == null)
            {
                return NoContent();
            }
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
