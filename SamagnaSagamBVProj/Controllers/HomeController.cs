using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SamagnaSagamBVProj.BusinessLogic;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SamagnaSagamBVProj
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {

        public ILogger _logger { get; }
        private readonly HomeServiceLogic _homeServiceLogic;

        public HomeController(ILogger<HomeController> logger, HomeServiceLogic homeServiceLogic)
        {
            _logger = logger;
            _homeServiceLogic = homeServiceLogic;
        }

        [HttpGet]
        public async Task<string> GetData()
        {
            try
            {
                _logger.LogInformation("Beginning the process of Home Controller API");
                return await _homeServiceLogic.GetDataAsync();
                
                
            }catch(Exception ex)

            {
                _logger.LogError("Unexpected error occurred" + ex.ToString() + ex.InnerException.ToString());
            }

            return string.Empty;
        }
    }
}
