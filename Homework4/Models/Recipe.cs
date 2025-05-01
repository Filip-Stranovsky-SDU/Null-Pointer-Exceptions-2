
using System.Collections.Generic;

namespace Homework4.Models;
public class Recipe {

    public string Name {get; set;}
    public string Difficulty {get; set;}
    public List<string> Equipment {get; set;}
    public List<StepClass> Steps {get; set;}

}