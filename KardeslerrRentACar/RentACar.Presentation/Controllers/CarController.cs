using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Application.Interfaces;
using RentACar.DTOs.Vehicle;

namespace RentACar.Presentation.Controllers
{
    [Route("[Controller]")]
    public class CarController : Controller
    {
        private readonly IVehicleService _vehicleService;
        public CarController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        private IActionResult Index(VehicleDTO vehicle)
        {
            return View(vehicle);
        }
        [HttpGet("Details/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            VehicleDTO? vehicle = await _vehicleService.GetVehicleDetailsAsync(id);
            if (vehicle == null) { return BadRequest(); }
            return View("Details", vehicle);
        }
        public async Task<IActionResult> AddVehicle([FromForm] VehicleDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            VehicleDTO? response = await _vehicleService.AddVehicleAsync(request);
            if (response == null) { return BadRequest(); }

            return RedirectToAction(nameof(Details), response);
        }
        public async Task<IActionResult> UpdateVehicle(int id, UpdateVehicleDTO vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            VehicleDTO? response = await _vehicleService.UpdateVehicleAsync(id, vehicle);
            if (response == null) { return BadRequest(); }
            return RedirectToAction(nameof(Details), response);
        }
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            bool response = await _vehicleService.DeleteVehicleAsync(id);
            if (response == false) { return NotFound(response); }
            return Ok(response);
        }
    }
}
