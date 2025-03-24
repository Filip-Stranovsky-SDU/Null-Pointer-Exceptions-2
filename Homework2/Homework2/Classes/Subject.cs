using System.Collections.Generic;
using System.Linq;

namespace Homework2.Classes;

public class Subject
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string TeacherId { get; private set; }
    public List<int> StudentsEnrolled { get; set; }

    public string TeacherName => SubjectHandler.GetUsers()
    .Where(u => u.Value is Teacher && u.Value.Id == int.Parse(TeacherId))
    .Select(u => u.Value.Name)
    .Single();

    public Subject(int id, string name, string description, string teacherId, List<int> studentsEnrolled)
    {
        Id = id;
        Name = name;
        Description = description;
        TeacherId = teacherId;
        StudentsEnrolled = studentsEnrolled;
    }
}