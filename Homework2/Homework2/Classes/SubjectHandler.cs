using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Reflection;

namespace Homework2.Classes;
public static class SubjectHandler
{
    private const string usersPath = "Homework2.Users.json";
    private const string subjectPath = "Homework2.Subjects.json";

    private static Dictionary<int, Subject> subjects = JsonSerializer.Deserialize<Dictionary<int, Subject>>(Assembly.GetExecutingAssembly().GetManifestResourceStream(subjectPath)!)!;
    private static Dictionary<string, User> users = JsonSerializer.Deserialize<Dictionary<string, User>>(Assembly.GetExecutingAssembly().GetManifestResourceStream(usersPath)!)!;

    public static void AddSubject(Subject subject)
    {
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
        subjects.Remove(subject.Id);
        string data = JsonSerializer.Serialize<Dictionary<int, Subject>>(subjects);
        File.WriteAllText(subjectPath, data);   
        foreach(var u in users)
        {
            if(u.GetType() == typeof(Student)) ((Student)u.Value).EnrolledSubjects.Remove(subject.Id);
            if(u.GetType() == typeof(Teacher)) ((Teacher)u.Value).EnrolledSubjects.Remove(subject.Id);
        }
        data = JsonSerializer.Serialize<Dictionary<string, User>>(users);
        File.WriteAllText(usersPath, data);
    

    }

    public static void StudentEnroll(User student, Subject subject)
    {
        ((Student) users[student.Username]).EnrolledSubjects.Add(subject.Id);
        string data = JsonSerializer.Serialize<Dictionary<string, User>>(users);
        File.WriteAllText(usersPath, data);
    }
    
    public static void StudentDrop(User student, Subject subject)
    {
        ((Student) users[student.Username]).EnrolledSubjects.Remove(subject.Id);
        string data = JsonSerializer.Serialize<Dictionary<string, User>>(users);
        File.WriteAllText(usersPath, data);
    }

    public static Dictionary<int, Subject> GetSubjects()
    {
        return subjects;
    }

    public static Dictionary<string, User> GetUsers() { return users; }




}