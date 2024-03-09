using Logistic.Models;
using Logistic.DAL.Interfaces;
using Logistic.Core.Interfaces;

namespace Logistic.Core
{
    public class WarehouseService : IEntityLoadingService<Warehouse>
    {
        public IInMemoryRepository<Warehouse> repository;

        public WarehouseService(IInMemoryRepository<Warehouse> repository)
        {
            this.repository = repository;
        }

        public Warehouse Create(Warehouse entity)
        {
            return repository.Create(entity);
        }

        public Warehouse Delete(int id)
        {
            return repository.Delete(id);
        }

        public List<Warehouse> GetAll()
        {
            return repository.ReadAll();
        }

        public Warehouse GetById(int id)
        {
            return repository.Read(id);
        }

        public Warehouse Update(int id, Warehouse entity)
        {
            return repository.Update(id, entity);
        }

        public Cargo LoadCargo(Cargo cargo, int idWarehouse)
        {
            var warehouseToLoad = repository.Read(idWarehouse);
            cargo.Id = Guid.NewGuid();
            warehouseToLoad.Stock.Add(cargo);
            repository.Update(idWarehouse, warehouseToLoad);
            return cargo;
        }

        public Cargo UnloadCargo(Guid guidCargo, int idWarehouse)
        {
            var warehouseToUnload = repository.Read(idWarehouse);
            var cargoToDelete = warehouseToUnload.Stock.FirstOrDefault(c => c.Id == guidCargo);

            if (cargoToDelete == null)
            {
                throw new Exception($"There is no cargo with id [ {guidCargo} ] in warehouse with id {warehouseToUnload.Id}");
            }

            warehouseToUnload.Stock.Remove(cargoToDelete);
            repository.Update(idWarehouse, warehouseToUnload);
            return cargoToDelete;
        }

        public Cargo UnloadLastCargo(int idWarehouse)
        {
            var warehouseToUnload = repository.Read(idWarehouse);
            if (!(warehouseToUnload.Stock.Count > 0))
            {
                throw new Exception($"There isn't any cargo in warehouse with id {warehouseToUnload.Id}");
            }

            var cargoToDelete = warehouseToUnload.Stock[warehouseToUnload.Stock.Count - 1];
            warehouseToUnload.Stock.Remove(cargoToDelete);
            repository.Update(idWarehouse, warehouseToUnload);
            return cargoToDelete;
        }
    }
}
