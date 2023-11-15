using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XComm.Api.Models;

namespace XComm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public static List<Student> StudentList = new List<Student>()
        {
            new Student(){Id = 1, FirstName = "Diaz", LastName = "Aditya", Age=17},
            new Student(){Id = 2, FirstName = "Auriwan", LastName = "Yasper", Age=22},
            new Student(){Id = 3, FirstName = "Niko", LastName = "Okin", Age=18}
        };

        [HttpGet]
        public async Task<List<Student>> Get()
        {
            return StudentList;
        }

        [HttpGet("{Id}")]
        public async Task<Student> Get(int id)
        {
            //Student student = StudentList.Where(o => o.Id == id).FirstOrDefault();
            //return student != null ? Student : new Student();
            return StudentList.Where(o => o.Id == id).First();
        }
    }
}
