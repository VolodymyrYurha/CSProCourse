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
            warehouseToLoad.Cargoes.Add(cargo);
            repository.Update(idWarehouse, warehouseToLoad);
            return cargo;
        }

        public Cargo UnloadCargo(Guid guidCargo, int idWarehouse)
        {
            var warehouseToUnload = repository.Read(idWarehouse);
            var cargoToDelete = warehouseToUnload.Cargoes.FirstOrDefault(c => c.Id == guidCargo);

            if (cargoToDelete == null)
            {
                throw new Exception($"There is no cargo with id [ {guidCargo} ] in warehouse with id {warehouseToUnload.Id}");
            }

            warehouseToUnload.Cargoes.Remove(cargoToDelete);
            repository.Update(idWarehouse, warehouseToUnload);
            return cargoToDelete;
        }

        public Cargo UnloadLastCargo(int idWarehouse)
        {
            var warehouseToUnload = repository.Read(idWarehouse);
            if (!(warehouseToUnload.Cargoes.Count > 0))
            {
                throw new Exception($"There isn't any cargo in warehouse with id {warehouseToUnload.Id}");
            }

            var cargoToDelete = warehouseToUnload.Cargoes[warehouseToUnload.Cargoes.Count - 1];
            warehouseToUnload.Cargoes.Remove(cargoToDelete);
            repository.Update(idWarehouse, warehouseToUnload);
            return cargoToDelete;
        }
    }
}
