using Microsoft.AspNetCore.Mvc;
using RentACar.Application.Interfaces;
using RentACar.DTOs.Vehicle;

namespace RentACar.Presentation.Controllers
{
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
        public async Task<IActionResult> GetDetailsAsync([FromRoute] int id)
        {
            VehicleDTO? vehicle = await _vehicleService.GetVehicleDetailsAsync(id);
            if (vehicle == null) { return NotFound(); }
            return RedirectToAction(nameof(Index), vehicle);
        }
        public async Task<IActionResult> AddVehicleAsync([FromForm] VehicleDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            VehicleDTO? response = await _vehicleService.AddVehicleAsync(request);
            if (response == null) { return BadRequest(); }

            return RedirectToAction(nameof(Index), response);
        }
        public async Task<IActionResult> UpdateVehicleAsync(int id, UpdateVehicleDTO vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            VehicleDTO? response = await _vehicleService.UpdateVehicleAsync(id, vehicle);
            if (response == null) { return BadRequest(); }
            return RedirectToAction(nameof(Index), response);
        }
        public async Task<IActionResult> DeleteVehicleAsync(int id)
        {
            bool response = await _vehicleService.DeleteVehicleAsync(id);
            if (response == false) { return NotFound(response); }
            return Ok(response);
        }
    }
}
