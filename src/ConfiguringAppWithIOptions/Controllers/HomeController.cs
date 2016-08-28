using ConfiguringAppWithIOptions.Configuration;
using ConfiguringAppWithIOptions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfiguringAppWithIOptions.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<OwnerOptions> _ownerOptions;
        private readonly IOptions<WebsiteOptions> _websiteOptions;
        private readonly IOptions<ConnectionStringOptions> _connectionStringOptions;

        public HomeController(IOptions<OwnerOptions> ownerOptions,
                              IOptions<WebsiteOptions> websiteOptions,
                              IOptions<ConnectionStringOptions> connectionStringOptions)
        {
            _ownerOptions = ownerOptions;
            _websiteOptions = websiteOptions;
            _connectionStringOptions = connectionStringOptions;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel
                        {
                            Owner = _ownerOptions.Value,
                            Website = _websiteOptions.Value,
                            ConnectionString = _connectionStringOptions.Value
                        });
        }

        public IActionResult Error() => View();
    }
}