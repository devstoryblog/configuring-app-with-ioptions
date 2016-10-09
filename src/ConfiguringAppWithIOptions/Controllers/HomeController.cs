using System.IO;
using ConfiguringAppWithIOptions.Configuration;
using ConfiguringAppWithIOptions.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfiguringAppWithIOptions.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<OwnerOptions> _ownerOptions;
        private readonly IOptions<WebsiteOptions> _websiteOptions;
        private readonly IOptions<ConnectionStringOptions> _connectionStringOptions;
        private readonly IOptions<YamlDataOptions> _yamlDataOptions;

        public HomeController(IOptions<OwnerOptions> ownerOptions,
                              IOptions<WebsiteOptions> websiteOptions,
                              IOptions<ConnectionStringOptions> connectionStringOptions,
                              IOptions<YamlDataOptions> yamlDataOptions)
        {
            _ownerOptions = ownerOptions;
            _websiteOptions = websiteOptions;
            _connectionStringOptions = connectionStringOptions;
            _yamlDataOptions = yamlDataOptions;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel
                        {
                            Owner = _ownerOptions.Value,
                            Website = _websiteOptions.Value,
                            ConnectionString = _connectionStringOptions.Value,
                            YamlData = _yamlDataOptions.Value
                        });
        }

        public IActionResult Error() => View();
    }
}