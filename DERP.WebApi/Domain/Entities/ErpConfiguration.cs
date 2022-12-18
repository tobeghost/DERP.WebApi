namespace DERP.WebApi.Domain.Entities;

public class ErpConfiguration
{
    public string HandlerClassAssembly { get; set; }
    public string HandlerClassName { get; set; }
    public string Type { get; set; } = "MSSQL";
    public string Host { get; set; }
    public string DatabaseName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public string ConnectionString
    {
        get
        {
            return Type switch
            {
                "MSSQL" => $"Data Source={Host}; Initial Catalog={DatabaseName}; User ID={Username}; Password={Password}; Connect Timeout=1000; MultipleActiveResultSets=True; Max Pool Size=200; Pooling=true;",
                "MYSQL" => $"Server={Host}; Database={DatabaseName}; Uid={Username}; Pwd={Password};",
                _ => ""
            };
        }
    }
}