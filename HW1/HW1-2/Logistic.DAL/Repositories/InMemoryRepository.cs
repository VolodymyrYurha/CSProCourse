using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logistic.DAL.Interfaces;
using Logistic.Models;
using Logistic.Models.Interfaces;

namespace Logistic.DAL
{
    public class InMemoryRepository<TEntity> : IInMemoryRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly string notFoundIdExceptionMessage = $"{typeof(TEntity).Name} entity with this Id doesn't exist";
        public List<TEntity> entities;
        private int currentID = 0;

        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, TEntity>());

        public InMemoryRepository()
        {
            entities = new List<TEntity>();
        }

        public InMemoryRepository(List<TEntity> entities)
        {
            this.entities = MapEntityList(entities);
            foreach (var entity in this.entities)
            {
                if (entity.Id == 0)
                {
                    entity.Id = NextId();
                }
            }
        }

        public TEntity Create(TEntity entity)
        {
            var entityCopy = MapEntity(entity);
            entityCopy.Id = NextId();
            //if (entityCopy.Id is int)
            //{
            //    entityCopy.Id = NextId();
            //}
            //if(entityCopy.Id is Guid)
            //{
            //    entityCopy.Id = Guid.NewGuid();
            //}
            entities.Add(entityCopy);
            return entityCopy;
        }

        public TEntity Delete(int id)
        {
            TEntity? entity = entities.FirstOrDefault(e => e.Id == id);
            if (entity == null)
            {
                throw new Exception(notFoundIdExceptionMessage + $"\t[Delete operation | id = {id}]");
            }

            entities.Remove(entity);
            return entity;
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

        public TEntity Update(int id, TEntity updateTo)
        {
            int index = entities.FindIndex(e => e.Id == id);

            if (index == -1)
            {
                throw new Exception(notFoundIdExceptionMessage + $"\t[Update operation | id = {id}]");
            }

            entities[index] = updateTo;
            entities[index].Id = id;
            return updateTo;
        }

        private int NextId()
        {
            return ++currentID;
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
