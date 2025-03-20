namespace Homework2.test;

using Homework2.Classes;

public class Homework2Tests
{
    private Dictionary<string, User> users = SubjectHandler.GetUsers();

    [Fact]
    public void PasswordHasherShouldHashPasswordCorrectly()
    {
        var password = "SecurePass123";

        var hash = PasswordHasher.HashPassword(password);

        Assert.NotEqual(password, hash); //password is hashed
        Assert.True(PasswordHasher.VerifyPassword(password, hash)); //hash verification works
    }

    [Fact]
    public void SubjectHandlerShouldManageStudentsEnrollments()
    {
        var student = (Student)users["student1"];
        var subject = SubjectHandler.GetSubjects()[1];

        SubjectHandler.StudentEnroll(student, subject);
        Assert.Contains(subject.Id, student.EnrolledSubjects); //student is enrolled

        SubjectHandler.StudentDrop(student, subject);
        Assert.DoesNotContain(subject.Id, student.EnrolledSubjects); //student drops from subject
    }

    [Fact]
    public void SubjectHandlerShouldRegisterSubjectsCorrectly()
    {
        var subject = SubjectHandler.GetSubjects()[1];

        SubjectHandler.DeleteSubject(subject);
        Assert.DoesNotContain(subject, SubjectHandler.GetSubjects().Select(s => s.Value)); //subject is deleted
        
        SubjectHandler.AddSubject(subject);
        Assert.Contains(SubjectHandler.GetSubjects().Values, s => //subject with correct values is added
            s.Id == subject.Id &&
            s.Name == subject.Name &&
            s.Description == subject.Description &&
            s.TeacherId == subject.TeacherId &&
            s.StudentsEnrolled.SequenceEqual(subject.StudentsEnrolled));
    }
}