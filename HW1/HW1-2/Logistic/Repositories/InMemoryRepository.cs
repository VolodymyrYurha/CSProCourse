using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logistic.ConsoleClient.Models.Interfaces;
using Logistic.ConsoleClient.Repositories.Interfaces;

namespace Logistic.ConsoleClient.Repositories
{
    public class InMemoryRepository<TEntity> : IRepositoryInMemory<TEntity>
        where TEntity : class, IEntity
    {
        private readonly string notFoundIdExceptionMessage = $"{typeof(TEntity).Name} entity with this Id doesn't exist";
        private List<TEntity> entities;

        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, TEntity>());

        // Constructors
        public InMemoryRepository()
        {
            entities = new List<TEntity>();
        }

        public InMemoryRepository(List<TEntity> entities)
        {
            this.entities = MapEntityList(entities);
            foreach (var entity in this.entities)
            {
                entity.Id = NextId();
            }
        }

        // Methods
        public void Create(TEntity entity)
        {
            var entityCopy = MapEntity(entity);
            entityCopy.Id = NextId();
            entities.Add(entityCopy);
        }

        public void Delete(int id)
        {
            TEntity? entity = entities.FirstOrDefault(e => e.Id == id);
            if (entity == null)
            {
                throw new Exception(notFoundIdExceptionMessage + $"\t[Delete operation | id = {id}]");
            }

            entities.Remove(entity);
        }

        public TEntity Read(int id)
        {
            TEntity? entity = entities.FirstOrDefault(e => e.Id == id);
            if (entity == null)
            {
                throw new Exception(notFoundIdExceptionMessage + $"\t[Read by id operation | id = {id}]");
            }

            return MapEntity(entity);
        }

        public List<TEntity> ReadAll()
        {
            return MapEntityList(entities);
        }

        public void Update(int id, TEntity updateTo)
        {
            int index = entities.FindIndex(e => e.Id == id);

            if (index == -1)
            {
                throw new Exception(notFoundIdExceptionMessage + $"\t[Update operation | id = {id}]");
            }

            entities[index] = updateTo;
            entities[index].Id = id;
        }

        // private methods
        private int NextId()
        {
            int? maxId = entities.Any() ? (int?)entities.Max(e => e.Id) : null;
            if (maxId != null)
            {
                return maxId.Value + 1;
            }

            return 1;
        }

        private TEntity MapEntity(TEntity originalEntity)
        {
            return config.CreateMapper().Map<TEntity>(originalEntity);
        }

        private List<TEntity> MapEntityList(List<TEntity> originalEntityList)
        {
            return originalEntityList.AsQueryable().ProjectTo<TEntity>(config).ToList();
        }
    }
}
