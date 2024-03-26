using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;
using Ponder.Settings;

namespace Ponder.Options;

public static class Dalie
{
    public async static void CreateNewDay(string dailieName, IOptions<Librarian> settings)
    {

        dailieName = ValidateDalieArgument(dailieName);

        // create necessary folders if they don't already exist
        if (!Directory.Exists(settings.Value.TemplatesPath))
        {
            Directory.CreateDirectory(settings.Value.TemplatesPath);
        }

        // create a new "default" template if it doesn't already exist
        if (!File.Exists(settings.Value.DefaultTemplatePath))
        {
            // copy file program's default file to the template folder
            File.Copy("Default.md", settings.Value.DefaultTemplatePath);
        }

        // copy template to new file named dailieName
        var newDailiePath = Path.Combine(settings.Value.DalieFolder, dailieName + ".md");
        File.Copy(settings.Value.DefaultTemplatePath, newDailiePath);

        // replace default values
        var template = File.ReadAllText(newDailiePath);
        template = template.Replace("<today>", dailieName);
        template = template.Replace("<last_modified>", DateTime.Now.ToString("dd-MM-yy HH:mm:ss"));
        File.WriteAllText(newDailiePath, template);
    }

    private static string ValidateDalieArgument(string input)
    {
        if (input.Trim() != "today")
        {
            throw new ArgumentException(nameof(input), "Input must be 'today'.");
        }

        return input.Trim();
    }
}

public class DalieException : Exception
{
    public DalieException(string message) : base(message) { }
}

public class DalieTemplateException : DalieException
{
    public DalieTemplateException(string message) : base(message) { }
}