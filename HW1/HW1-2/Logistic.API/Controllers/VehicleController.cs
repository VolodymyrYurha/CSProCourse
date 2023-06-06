using Logistic.Core;
using Logistic.Core.Interfaces;
using Logistic.DAL;
using Logistic.Models;
using Logistic.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {

        private readonly IEntityLoadingService<Vehicle> _vehicleService;

        public VehicleController(IEntityLoadingService<Vehicle> vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public IActionResult Create(Vehicle vehicle)
        {
            return Ok(_vehicleService.Create(vehicle));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_vehicleService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var vehicle = _vehicleService.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Vehicle updatedVehicle)
        {
            var vehicle = _vehicleService.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(_vehicleService.Update(id, updatedVehicle));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vehicle = _vehicleService.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(_vehicleService.Delete(id));
        }

    }
}