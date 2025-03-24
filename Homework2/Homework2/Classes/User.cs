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
    public string Username {get; init;} = null!;
    public string Password {get; init;} = null!;

    public bool LoginCheck(string username, string password)
    {
        return this.Username == username
        && PasswordHasher.VerifyPassword(password, this.Password);

    }
    
}
