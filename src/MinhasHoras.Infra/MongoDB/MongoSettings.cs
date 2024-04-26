namespace MinhasHoras.Infra.MongoDB
{
    public class MongoSettings
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
    }
}
