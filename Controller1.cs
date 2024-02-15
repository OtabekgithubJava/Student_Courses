using Microsoft.AspNetCore.Mvc;
using Dapper;
using Npgsql;


using System;
using System.Collections.Generic;
using BlazorApp1;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        public string connectionString = "Server=localhost;Port=5432;Database=Courses;username=postgres;Password=2712;";

        [HttpGet]
        public List<Student> GetListStudents()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Student;";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                var result = cmd.ExecuteReader();

                List<Student> list = new List<Student>();

                while (result.Read())
                {
                    list.Add(new Student
                    {
                        id = (int)result[0],
                        name = (string)result[1],
                        courses = GetStudentCourses((int)result[0])
                    });
                }
                return list;
            }
        }

        private List<Course> GetStudentCourses(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT c.id, c.title FROM Student s JOIN StudentCourse sc ON sc.student_id = s.id JOIN Course c ON sc.course_id = c.id WHERE s.id = {id};";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                var result = cmd.ExecuteReader();

                List<Course> list = new List<Course>();

                while (result.Read())
                {
                    list.Add(new Course
                    {
                        id = (int)result[0],
                        name = (string)result[1]
                    });
                }
                return list;
            }
        }

    }
}