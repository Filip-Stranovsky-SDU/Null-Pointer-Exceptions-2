using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Homework2.Classes;

public class Student : User
{
    public List<int> EnrolledSubjects { get; set; } = null!;
    
    public Student(int id, string name, string username, string password, List<int> enrolledSubjects)
    {
        Id = id;
        Name = name;
        Username = username;
        Password = password;
        EnrolledSubjects = enrolledSubjects;
    }
}