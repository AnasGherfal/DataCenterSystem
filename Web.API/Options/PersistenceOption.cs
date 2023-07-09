namespace Web.API.Options;

public class PersistenceOption
{
    public const string Section = "Persistence";
    public string ConnectionString { get; set; } = string.Empty;
}