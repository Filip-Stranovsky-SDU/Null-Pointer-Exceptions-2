using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Homework2.Classes;

public class Student : User
{
    public List<int> EnrolledSubjects { get; set; } = null!;
    

}