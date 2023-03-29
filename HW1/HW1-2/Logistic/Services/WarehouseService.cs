using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Repositories.Interfaces;
using Logistic.ConsoleClient.Services.Interfaces;

namespace Logistic.ConsoleClient.Services
{
    public class WarehouseService : IEntityLoadingService<Warehouse>
    {
        private IInMemoryRepository<Warehouse> repository;

        public WarehouseService(IInMemoryRepository<Warehouse> repository)
        {
            this.repository = repository;
        }

        public void Create(Warehouse entity)
        {
            repository.Create(entity);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public List<Warehouse> GetAll()
        {
            return repository.ReadAll();
        }

        public Warehouse GetById(int id)
        {
            return repository.Read(id);
        }

        public void LoadCargo(Cargo cargo, int idWarehouse)
        {
            var warehouseToLoad = repository.Read(idWarehouse);
            warehouseToLoad.Stock.Add(cargo);
            repository.Update(idWarehouse, warehouseToLoad);
        }

        public void UnloadCargo(Guid guidCargo, int idWarehouse)
        {
            var warehouseToUnload = repository.Read(idWarehouse);
            var cargoToDelete = warehouseToUnload.Stock.FirstOrDefault(c => c.Id == guidCargo);

            if (cargoToDelete == null)
            {
                throw new Exception($"There is no cargo with id [ {guidCargo} ] in warehouse with id {warehouseToUnload.Id}");
            }

            warehouseToUnload.Stock.Remove(cargoToDelete);
            repository.Update(idWarehouse, warehouseToUnload);
        }

        public void UnloadLastCargo(int idWarehouse)
        {
            var warehouseToUnload = repository.Read(idWarehouse);
            if (!(warehouseToUnload.Stock.Count > 0))
            {
                throw new Exception($"There isn't any cargo in warehouse with id {warehouseToUnload.Id}");
            }

            var cargoToDelete = warehouseToUnload.Stock[warehouseToUnload.Stock.Count - 1];
            warehouseToUnload.Stock.Remove(cargoToDelete);
            repository.Update(idWarehouse, warehouseToUnload);
        }
    }
}
