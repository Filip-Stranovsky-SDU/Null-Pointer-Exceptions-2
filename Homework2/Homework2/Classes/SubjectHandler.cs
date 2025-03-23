using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Homework2.Classes;
public static class SubjectHandler
{
    private const string usersPath = "Homework2.Users.json";
    private const string subjectPath = "Homework2.Subjects.json";

    private static string localUsersPath = ResourceExtractor.ExtractResource(usersPath);
    private static string localSubjectPath = ResourceExtractor.ExtractResource(subjectPath);

    private static Dictionary<int, Subject> subjects = JsonSerializer.Deserialize<Dictionary<int, Subject>>(File.ReadAllText(localSubjectPath))!;
    private static Dictionary<string, User> users = JsonSerializer.Deserialize<Dictionary<string, User>>(File.ReadAllText(localUsersPath))!;

    private static void UpdateUsers()
    {
        string data = JsonSerializer.Serialize<Dictionary<string, User>>(users);
        File.WriteAllText(localUsersPath, data);
    }

    private static void UpdateSubjects()
    {
        string data = JsonSerializer.Serialize<Dictionary<int, Subject>>(subjects);
        File.WriteAllText(localSubjectPath, data);
    }

    private static void UpdateFiles()
    {
        UpdateUsers();
        UpdateSubjects();
    }

    public static void AddSubject(Subject subject)
    {
        subjects[subject.Id] = subject;

        UpdateSubjects();

    }

    public static void EditSubject(Subject subject)
    {
        AddSubject(subject);
    }
    public static void DeleteSubject(Subject subject)
    {
        subjects.Remove(subject.Id);
 
        foreach(var u in users)
        {
            if(u.GetType() == typeof(Student)) ((Student)u.Value).EnrolledSubjects.Remove(subject.Id);
            if(u.GetType() == typeof(Teacher)) ((Teacher)u.Value).EnrolledSubjects.Remove(subject.Id);
        }
        UpdateFiles();


    }

    public static void StudentEnroll(User student, Subject subject)
    {
        ((Student) users[student.Username]).EnrolledSubjects.Add(subject.Id);
        subjects[subject.Id].StudentsEnrolled.Add(student.Id);
        UpdateFiles();
    }
    
    public static void StudentDrop(User student, Subject subject)
    {
        ((Student) users[student.Username]).EnrolledSubjects.Remove(subject.Id);
        subjects[subject.Id].StudentsEnrolled.Remove(student.Id);
        UpdateFiles();
    }

    public static Dictionary<int, Subject> GetSubjects()
    {
        return subjects;
    }

    public static Dictionary<string, User> GetUsers() { return users; }




}