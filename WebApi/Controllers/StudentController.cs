using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]")]

    public class StudentController : ControllerBase
    {
        private static List<Student> StudentList = new List<Student>()
        {
            new Student{
                Number = 385,
                Name="Gülizar",
                Age=28,
                Score=98,
                
            },
             new Student{
                Number = 245,
                Name="İsmail",
                Age=25,
                Score=85,
               
            },
             new Student{
                Number = 342,
                Name="Yaren",
                Age=26,
                Score=78,
                
            }
        };

        // Yorum satırında olan requestler çalışıyor. Test ederken yorumları kaldırınız

        [HttpGet]
        public List<Student> GetStudents()
        {                                //alfabetik sırada
            var studentList =StudentList.OrderBy(x =>x.Name).ToList<Student>();
            return studentList;
        }

       [HttpGet("{number}")]
        public Student GetByNumber(int number)
        {
            var student = StudentList.Where(student => student.Number == number).FirstOrDefault(); //singleordefult hata veriyor
            return student;
        }

/*
        [HttpGet]
        public Student Get([FromQuery] string number)
        {
            var student = StudentList.Where(student => student.Number ==Convert.ToInt32(number)).SingleOrDefault();
            return student;
        }
*/


        
       /* [HttpPost]
        public IActionResult AddStudent([FromBody] Student newStudent)
        {
            var student = StudentList.SingleOrDefault(x => x.Name == newStudent.Name);

            if(student is not null)   //listemde varsa bad reuest dön
                return BadRequest();
            StudentList.Add(newStudent);
            return Ok();
            

        }
    */

        //---------------BONUSSS------------------------------
          [HttpPost]
    public IActionResult AddStudent([FromQuery] int number, [FromQuery] string name, [FromQuery] int age, [FromQuery] int score, [FromBody] Student newStudent)
    {
        // Hem sorgu parametrelerinden hem de gövdeden alındı
        var student = StudentList.SingleOrDefault(x => x.Name == newStudent.Name);

        if (student is not null)
        {
            // Listede varsa bad request dön
            return BadRequest("Student with the same name already exists.");
        }

        // Sorgu parametreleri veya gövde içeriği ile yeni öğrenci oluştur
        var addedStudent = new Student
        {
            Number = number,
            Name = name ?? newStudent.Name,
            Age = age > 0 ? age : newStudent.Age,
            Score =score
        };

        StudentList.Add(addedStudent);

        return Ok();
    }

        [HttpPut("{number}")]
        public IActionResult UpdateStudent(int number, [FromBody] Student updatedStudent)
        {
            var student = StudentList.SingleOrDefault(x=> x.Number == number);

            if(student is null)
                return BadRequest();
            student.Name = updatedStudent.Name != default ? updatedStudent.Name : student.Name;
            student.Age = updatedStudent.Age != default ? updatedStudent.Age : student.Age;
            student.Score = updatedStudent.Score != default ? updatedStudent.Score : student.Score;
            

            return Ok();

        }

         [HttpPatch("{number}")]
        public IActionResult UpdateStudentName(int number, [FromBody] Student updatedStudent)
        {
            var student = StudentList.SingleOrDefault(x=> x.Number == number);

            if(student is null)
                return BadRequest();
            student.Name = updatedStudent.Name != default ? updatedStudent.Name : student.Name;

            return Ok();

        }


        [HttpDelete("{number}")]
        public IActionResult DeleteStudent(int number)
        {
            var student = StudentList.SingleOrDefault (x => x.Number == number);
            if(student is null)
                return BadRequest();
            
            StudentList.Remove(student);
            return Ok();
        }

        
    }

}