using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace WebAPIDBOperations.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        AppDbContext _db;
        public ProductController(AppDbContext db)
        {
            _db = db;
        }

        //GET: api/product/getall
        //[HttpGet]
        //public async Task<IEnumerable<Product>> GetAll()
        //{
        //    return await _db.Products.ToListAsync();
        //}

        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        //[HttpGet]
        //public IActionResult GetAll(int version = 1)
        //{
        //    if (version == 2)
        //    {
        //        var data = (from prd in _db.Products
        //                    join cat in _db.Categories
        //                    on prd.CategoryId equals cat.CategoryId
        //                    select new
        //                    {
        //                        ProductId = prd.ProductId,
        //                        ProductName = prd.Name,
        //                        UnitePrice = prd.UnitPrice,
        //                        Name = cat.Name
        //                    }).ToList();
        //        return Ok(data);
        //    }
        //    var products = _db.Products;
        //    return Ok(products);
        //}

        //GET: api/product/get/{id}
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _db.Products.Find(id);
        }

        //POST: api/product/add
        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Add(Product model)
        {
            try
            {
                _db.Products.Add(model);
                _db.SaveChanges();
                return CreatedAtAction("Add", model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //PUT: api/product/update/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, Product model)
        {
            try
            {
                if (id != model.ProductId)
                    return BadRequest();

                _db.Products.Update(model);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //DELETE: api/product/delete
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                Product model = _db.Products.Find(id);
                if (model != null)
                {
                    _db.Products.Remove(model);
                    _db.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
