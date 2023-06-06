using Logistic.Core;
using Logistic.Core.Interfaces;
using Logistic.DAL;
using Logistic.Models;
using Logistic.Models.Enums;
using Logistic.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace Logistic.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {

        private readonly IEntityLoadingService<Vehicle> _vehicleService;
        private readonly IReportService<Vehicle> _reportService;

        public VehicleController(IEntityLoadingService<Vehicle> vehicleService, IReportService<Vehicle> reportService)
        {
            _vehicleService = vehicleService;
            _reportService = reportService;
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

        [HttpPost("{id}/load")]
        public Cargo LoadCargo(int id, Cargo cargo)
        {
            return _vehicleService.LoadCargo(cargo, id);
        }

        [HttpPost("{vehicleId}/unload/{cargoGuid}")]
        public Cargo UnloadCargo(Guid cargoGuid, int vehicleId)
        {
            return _vehicleService.UnloadCargo(cargoGuid, vehicleId);
        }

        [HttpGet("createreport/{type}")]
        public IActionResult CreateReport(ReportType type)
        {
            var vehicles = _vehicleService.GetAll();
            _reportService.CreateReport(type, vehicles);
            return Ok();
        }

        [HttpGet("download/{file}")]
        public IActionResult DownloadFile(string file)
        {
            if (file.EndsWith(".json"))
            {
                return File(_reportService.LoadFile(file), "application/json", "report.json");
            }
            else
            {
                return File(_reportService.LoadFile(file), "application/xml", "report.xml");
            }
        }
    }
}