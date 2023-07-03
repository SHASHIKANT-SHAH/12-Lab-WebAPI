using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIDBOperations.Controllers
{
    [Route("api/v2/product/[action]")]
    [ApiController]
    public class ProductV2Controller : ControllerBase
    {
        AppDbContext _db;
        public ProductV2Controller(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = (from prd in _db.Products
                        join cat in _db.Categories
                        on prd.CategoryId equals cat.CategoryId
                        select new
                        {
                            ProductId = prd.ProductId,
                            ProductName = prd.Name,
                            UnitePrice = prd.UnitPrice,
                            Name = cat.Name
                        }).ToList();
            return Ok(data);
        }
    }
}
