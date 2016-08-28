using ConfiguringAppWithIOptions.Configuration;

namespace ConfiguringAppWithIOptions.Models
{
    public class IndexViewModel
    {
        public OwnerOptions Owner { get; set; }

        public WebsiteOptions Website { get; set; }

        public ConnectionStringOptions ConnectionString { get; set; }
    }
}