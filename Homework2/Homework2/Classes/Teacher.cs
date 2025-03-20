using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Homework2.Classes;

public class Teacher : User
{
    public List<int> EnrolledSubjects { get; private set; } = null!;

}