using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace randomWord.Controllers
{
    public class HomeController : Controller
    {
        
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.word = HttpContext.Session.GetString("random");
            ViewBag.count = HttpContext.Session.GetInt32("count");
            return View();
        }

        [HttpGet]
        [Route("process")]
        public IActionResult Process()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string rand_word = new string(Enumerable.Repeat(chars, 14).Select(s => s[random.Next(s.Length)]).ToArray());
            HttpContext.Session.SetString("random", rand_word);
            if (HttpContext.Session.GetInt32("count") == null)
            {
                HttpContext.Session.SetInt32("count", 1);
            }
            else
            {
                int num_attempts = (int) HttpContext.Session.GetInt32("count");
                num_attempts ++;
                HttpContext.Session.SetInt32("count", num_attempts); 
            }
            return RedirectToAction("Index");       
        }
    }
}
