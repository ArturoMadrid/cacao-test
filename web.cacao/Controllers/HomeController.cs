using Microsoft.AspNetCore.Mvc;
using models.cacao;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using web.cacao.Data;
using web.cacao.Models;

namespace web.cacao.Controllers
{
    public class HomeController : Controller
    {
        static int totalStudentAllowed = 0;
        ConsumeApi objApi = new ConsumeApi();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Students()
        {
            var resp = objApi.GetStudents();
            return Json(new { data = resp, totalStudentAllowed });
        }

        [HttpPost]
        public JsonResult SaveStudent([FromBody] Student data)
        {
            var resp = objApi.SaveStudent(data);
            return Json(resp);
        }

        [HttpPost]
        public JsonResult AllowedStudetn(int allowed)
        {
            totalStudentAllowed = allowed;
            return Json(new { data = true });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
