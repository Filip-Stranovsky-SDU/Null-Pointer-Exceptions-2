using System.Collections.Generic;

namespace Homework2.Classes;

public class Subject
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string TeacherId { get; private set; }
    public List<int> StudentsEnrolled { get; set; }

    public Subject(int id, string name, string description, string teacherId, List<int> studentsEnrolled)
    {
        Id = id;
        Name = name;
        Description = description;
        TeacherId = teacherId;
        StudentsEnrolled = studentsEnrolled;
    }
}