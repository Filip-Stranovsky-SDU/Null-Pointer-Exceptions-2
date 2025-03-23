using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Homework2.Classes;

public static class ResourceExtractor
{
    public static string ExtractResource(string resourceName)
    {
        //construct the output path dynamically based on the resource name
        string fileName = resourceName.Substring(resourceName.LastIndexOf('.',resourceName.LastIndexOf('.') - 1) + 1); //extracts e.g. "Users.json" from "Homework2.Users.json"
        string outputPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), //windows: C:\Users\{username}\AppData\Roaming; linux: /home/{username}/.config; mac: /Users/{username}/Library/Application Support
            "Homework2",
            fileName
        );

        string directory = Path.GetDirectoryName(outputPath)!;

        //ensure the directory exists
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        //extract only if the file doesn't already exist
        if (!File.Exists(outputPath))
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)) //content of embedded resource
            using (var file = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) //create new file
            {
                if (resource == null)
                {
                    throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");
                }
                resource.CopyTo(file); //copy content of embedded resource to new file
            }
        }

        return outputPath;
    }
}
