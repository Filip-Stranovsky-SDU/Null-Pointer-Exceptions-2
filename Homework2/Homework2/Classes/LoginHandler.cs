using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Homework2.Classes;

public static class LoginHandler
{
    private const string filePath = "Homework2.Users.json";

    //private const JsonSerializerSettings settings = new JsonSerializerSettings {PropertyNameCaseInsesitive = false};

    public static User? LoginHandle(string username, string password)
    {
        if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null;
        Dictionary<string, User> users = JsonSerializer.Deserialize<Dictionary<string, User>>(Assembly.GetExecutingAssembly().GetManifestResourceStream(filePath)!)!;
        if ( !users.ContainsKey(username)) return null;
        if ( !users[username].LoginCheck(username, password) ) return null;

        return users[username];

        
    }   



}

internal class JsonSerializerSettings
{
}