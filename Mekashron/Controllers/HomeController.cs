using Mekashron.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Mekashron.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string inputEmail, string inputPassword, string firstName, string lastName, string mobile)
        {
            ServiceReference1.RegisterNewCustomerRequest logReg = new ServiceReference1.RegisterNewCustomerRequest(inputEmail, inputPassword, firstName,lastName, mobile, 1,1,"");
            ServiceReference1.ICUTechClient iCUTechClient = new ServiceReference1.ICUTechClient();
            ServiceReference1.RegisterNewCustomerResponse result = await iCUTechClient.RegisterNewCustomerAsync(logReg);
            var resultCode = JsonConvert.DeserializeObject<ResultViewModel>(result.@return);
            return View(new ResultViewModel { ResultCode = resultCode.ResultCode, ResultString = result.@return });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string inputEmail, string inputPassword)
        {
            ServiceReference1.LoginRequest logReg = new ServiceReference1.LoginRequest(inputEmail, inputPassword, "");
            ServiceReference1.ICUTechClient iCUTechClient = new ServiceReference1.ICUTechClient();
            ServiceReference1.LoginResponse result = await iCUTechClient.LoginAsync(logReg);
            var resultCode = JsonConvert.DeserializeObject<ResultViewModel>(result.@return);

            return View(new ResultViewModel { ResultCode = resultCode.ResultCode, ResultString = result.@return });
        }
    }
}