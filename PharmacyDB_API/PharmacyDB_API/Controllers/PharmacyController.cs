using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyDB_API.Data;
using PharmacyDB_API.Models;

namespace PharmacyDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly ApiContext _context;

        public PharmacyController(ApiContext context) { _context = context; }

        [HttpPost]
        public JsonResult CreateEdit(string pharmName)
        {
            if (_context.Pharms.Find(pharmName) != null) 
            {
                var pharmaceuticalInDb = _context.Pharms.Find(pharmName);
                pharmaceuticalInDb.Name = pharmName;
            } else
            {
                Pharmaceutical newPharm = new Pharmaceutical();
                
                newPharm.Id = _context.Pharms.Last().Id + 1;
                newPharm.Name = pharmName;

                _context.Pharms.Add(newPharm);
            }

            _context.SaveChanges();

            return new JsonResult(Ok(pharmName));
        }

        [HttpGet]
        public JsonResult Get(int id) 
        {
            var result = _context.Pharms.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Pharms.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _context.Pharms.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        [HttpGet("/GetAll")]
        public JsonResult GetAll()
        {
            var result = _context.Pharms.ToList();

            return new JsonResult(Ok(result));
        }
    }
}
