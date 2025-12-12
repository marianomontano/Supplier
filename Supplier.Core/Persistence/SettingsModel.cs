namespace Supplier.Core.Persistence;

public class SettingsModel
{
    public int Id { get; set; }
    public int Markup { get; set; }
    public string ApiUrl { get; set; }
    public string ExportPath { get; set; }
}
