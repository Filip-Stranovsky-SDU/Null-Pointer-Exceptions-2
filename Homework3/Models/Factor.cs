using CsvHelper.Configuration.Attributes;

namespace homework3_livecharts.Models;

public class Factor
{
    [Name("Hours_Studied")]
    public int Hours_Stidied { get; set; }
    
    [Name("Attendance")]
    public int Attendance { get; set; }
    
    [Name("Parental_Involvement")]
    public string Parental_Involvement { get; set; }
    
    [Name("Access_to_Resources")]
    public string Access_to_Resources { get; set; }
    
    [Name("Extracurricular_Activities")]
    public string Extracurricular_Activities { get; set; }
    
    [Name("Sleep_Hours,Previous_Scores")]
    public int Sleep_Hours { get; set; }
    
    [Name("Motivation_Level")]
    public int Motivation_Level { get; set; }
    
    [Name("Internet_Access")]
    public string Internet_Access { get; set; }
    
    [Name("Tutoring_Sessions")]
    public string Tutoring_Sessions { get; set; }
    
    [Name("Family_Income")]
    public int Family_Income { get; set; }
    
    [Name("Teacher_Quality")]
    public string Teacher_Quality { get; set; }
    
    [Name("School_Type")]
    public string School_Type { get; set; }
    
    [Name("Peer_Influence")]
    public string Peer_Influence { get; set; }
    
    [Name("Physical_Activity")]
    public string Physical_Activity { get; set; }
    
    [Name("Learning_Disabilities")]
    public int Learning_Disabilities { get; set; }
    
    [Name("Parental_Education_Level")]
    public string Parental_Education_Level { get; set; }
    
    [Name("Distance_from_Home")]
    public string Distance_from_Home { get; set; }
    
    [Name("Gender")]
    public string Gender { get; set; }
    
    [Name("Exam_Score")]
    public int Exam_Score { get; set; }
}