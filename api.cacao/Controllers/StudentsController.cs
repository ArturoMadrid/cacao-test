using business.cacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using models.cacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.cacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        readonly StudentCore core = new StudentCore();

        [HttpGet]
        public IActionResult Get()
        {
            List<Student> students = core.GetStudents();

            return Ok(new { success = true, data = students });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Student student = core.GetStudent(id);

            return Ok(new { success = true, data = student });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            core.DeleteStudent(id);

            return Ok(new { success = true });
        }

        [HttpPost]
        public IActionResult Post([FromBody] Student _info)
        {
            core.AddStudent(_info);

            return Ok(new { success = true });
        }
    }
}
