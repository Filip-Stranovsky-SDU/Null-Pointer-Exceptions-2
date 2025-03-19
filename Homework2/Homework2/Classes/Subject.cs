using System.Collections.Generic;

namespace HeatOptimiser.Classes;

public class Subject
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string TeacherId { get; private set; }
    public List<int> StudentsEnrolled { get; set; }
}