using Microsoft.AspNetCore.Mvc;
using RentACar.Presentation.Models;
using System.Diagnostics;
using RentACar.Application.Interfaces;
using RentACar.DTOs.Vehicle;
using Microsoft.IdentityModel.Tokens;

namespace RentACar.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleService _vehicleService;
        public HomeController(ILogger<HomeController> logger, IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<ListVehicleDTO> listVehicleDto = await _vehicleService.GetVehiclesAsync();
            if (listVehicleDto.IsNullOrEmpty()) { return View(NoContent()); }
            return View(listVehicleDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
