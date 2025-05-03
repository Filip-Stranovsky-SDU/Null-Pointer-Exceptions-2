
using System;
using System.Collections.Generic;
using System.Reactive;

namespace Homework4.Models;
public class Recipe {

    public string Name {get; set;}
    public string Difficulty {get; set;}
    public List<string> Equipment {get; set;}
    public List<StepClass> Steps {get; set;}

    public override string ToString()
    {
        return $"{DateTime.Now},{Name}";
    }
}