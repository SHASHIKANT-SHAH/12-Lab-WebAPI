using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApp.Models;
using System.Text;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        IConfiguration _config;
        HttpClient client;
        public ProductController(IConfiguration config)
        {
            _config = config;
            client = new HttpClient();
            var uri = new Uri(_config["ApiAddress"]);
            client.BaseAddress = uri;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductModel> model = new List<ProductModel>();
            var response = client.GetAsync(client.BaseAddress + "/product/getall").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                model = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(data);
            }
            return View(model);
        }
        IEnumerable<CategoryMoodel> GetCategoies()
        {
            IEnumerable<CategoryMoodel> model = new List<CategoryMoodel>();
            var response = client.GetAsync(client.BaseAddress + "/category/getall").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                model = JsonSerializer.Deserialize<IEnumerable<CategoryMoodel>>(data);
            }
            return model;
        }
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategoies();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            string strData = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");

            var response = client.PostAsync(client.BaseAddress+"/product/add",content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Categories = GetCategoies();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Categories = GetCategoies();
            ProductModel model = new ProductModel();
            var response = client.GetAsync(client.BaseAddress + "/product/get/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                model = JsonSerializer.Deserialize<ProductModel>(data);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            string strData = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");

            var response = client.PutAsync(client.BaseAddress + "/product/update/"+model.ProductId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Categories = GetCategoies();
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var response = client.DeleteAsync(client.BaseAddress + "/product/delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
