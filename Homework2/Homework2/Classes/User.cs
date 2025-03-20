using System;
using System.Text.Json.Serialization;

namespace  Homework2.Classes;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Student), typeDiscriminator: "student")]
[JsonDerivedType(typeof(Teacher), typeDiscriminator: "teacher")]
public abstract class User
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = null!;
    protected string username = null!;
    protected string password = null!;

    public bool LoginCheck(string username, string password)
    {
        return this.username == username
        && PasswordHasher.VerifyPassword(password, this.password);

    }
    
}
