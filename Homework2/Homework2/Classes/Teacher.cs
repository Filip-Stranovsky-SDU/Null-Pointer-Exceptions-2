using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Homework2.Classes;

public class Teacher : User
{
    public List<int> EnrolledSubjects { get; private set; }

    [JsonConstructor]
    public Teacher(List<int> EnrolledSubjects,
                    int Id,
                    string Name,
                    string username,
                    string password)
    {
        this.Id = Id;
        this.Name = Name;
        this.username = username;
        this.password = password;
        this.EnrolledSubjects = EnrolledSubjects ?? new List<int>();
    }

    
}