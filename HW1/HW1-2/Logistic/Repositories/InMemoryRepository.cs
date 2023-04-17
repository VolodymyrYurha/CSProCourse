﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logistic.ConsoleClient.Repositories.Interfaces;
using Logistic.Models.Interfaces;

namespace Logistic.ConsoleClient.Repositories
{
    public class InMemoryRepository<TEntity> : IInMemoryRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly string notFoundIdExceptionMessage = $"{typeof(TEntity).Name} entity with this Id doesn't exist";
        private List<TEntity> entities;
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
