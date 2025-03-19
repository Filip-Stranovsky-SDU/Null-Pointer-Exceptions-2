using System.Collections.Generic;

namespace HeatOptimiser.Classes;

public class Student
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    private string username;
    private string password;
    public List<int> EnrolledSubjects { get; set; }

    public Student(int id, string name, string username, string password)
    {
        Id = id;
        Name = name;
        this.username = username;
        this.password = password;
        
    }

    public bool LoginCheck(string username, string password)
    {
        return this.username == username && this.password == password;
    }
}