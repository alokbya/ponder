namespace Ponder.Settings;

public class Librarian
{
    public string DefaultWorkspace { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Ponder");
    public string DalieFolder { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Ponder", "Dalies");
    public string TemplateFolder { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Ponder", "Templates");
    public string DefaultTemplatePath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Ponder", "Templates", "Default.md");
}
