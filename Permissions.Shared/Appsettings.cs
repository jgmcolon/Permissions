namespace Permissions.Shared
{
    public class Appsettings
    {
        public Streaming? Streaming { get; set; }
        public IndexSearch? IndexSearch { get; set; }
        public ConnectionStrings? ConnectionStrings { get; set; }

    }

    public class Streaming {
        public string? Source { get; set; }
        public string? BootstrapServers { get; set; }
        public string? ClientId { get; set; }
    }

    public class IndexSearch
    {
        public string? Source { get; set; }
        public string? Url { get; set; }

    }

    public class ConnectionStrings { 
         public string DefaultConnection {get; set;}
    }
}
