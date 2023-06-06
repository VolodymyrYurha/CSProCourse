using Logistic.Core;
using Logistic.Core.Interfaces;
using Logistic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : Controller
    {
        private readonly IEntityLoadingService<Warehouse> _warehouseService;

        public WarehouseController(IEntityLoadingService<Warehouse> warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost]
        public IActionResult Create(Warehouse warehouse)
        {
            return Ok(_warehouseService.Create(warehouse));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_warehouseService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var warehouse = _warehouseService.GetById(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return Ok(warehouse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Warehouse updatedWarehouse)
        {
            var warehouse = _warehouseService.GetById(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return Ok(_warehouseService.Update(id, updatedWarehouse));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var warehouse = _warehouseService.GetById(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return Ok(_warehouseService.Delete(id));
        }
    }
}
