using BlazorApp1;

public class Course
{
    public int id { get; set; }
    public string name { get; set; }
    public List<StudentCourse> studentCourses { get; set; }
}