

using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Homework2.Classes;
public static class CoursesHandler
{
    private const string usersPath = "/Users/matus/Desktop/HomeWork/Homework2/Homework2/Users.json";
    private const string subjectPath = "./Subjects.json";


    public static void AddSubject(Subject subject)
    {
        Dictionary<int, Subject> subjects = JsonSerializer.Deserialize<Dictionary<int, Subject>>(File.ReadAllText(subjectPath))!;

        subjects[subject.Id] = subject;

        string data = JsonSerializer.Serialize<Dictionary<int, Subject>>(subjects);

        File.WriteAllText(subjectPath, data);
        
    }

    public static void EditSubject(Subject subject)
    {
        AddSubject(subject);
    }
    public static void DeleteSubject(Subject subject)
    {
        Dictionary<int, Subject> subjects = JsonSerializer.Deserialize<Dictionary<int, Subject>>(File.ReadAllText(subjectPath))!;
        subjects.Remove(subject.Id);
        string data = JsonSerializer.Serialize<Dictionary<int, Subject>>(subjects);
        File.WriteAllText(subjectPath, data);
        Dictionary<string, User> users = JsonSerializer.Deserialize<Dictionary<string, User>>(File.ReadAllText(usersPath))!;
        foreach(var u in users)
        {
            ((Student)u.Value).EnrolledSubjects.Remove(subject.Id);
        }
        data = JsonSerializer.Serialize<Dictionary<string, User>>(users);
        File.WriteAllText(usersPath, data);
    

    }

    public static void StudentEnroll(Student student, Subject subject)
    {
        Dictionary<string, User> users = JsonSerializer.Deserialize<Dictionary<string, User>>(File.ReadAllText(usersPath))!;
        ((Student) users[student.Username]).EnrolledSubjects.Add(subject.Id);
        string data = JsonSerializer.Serialize<Dictionary<string, User>>(users);
        File.WriteAllText(usersPath, data);
    }
    
    public static void StudentDrop(Student student, Subject subject)
    {
        Dictionary<string, User> users = JsonSerializer.Deserialize<Dictionary<string, User>>(File.ReadAllText(usersPath))!;
        ((Student) users[student.Username]).EnrolledSubjects.Remove(subject.Id);
        string data = JsonSerializer.Serialize<Dictionary<string, User>>(users);
        File.WriteAllText(usersPath, data);
    }

    public static Dictionary<int, Subject> GetSubjects()
    {
        Dictionary<int, Subject> subjects = JsonSerializer.Deserialize<Dictionary<int, Subject>>(File.ReadAllText(subjectPath))!;
        return subjects;
    }

    




}