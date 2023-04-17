﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistic.Models.Interfaces;

namespace Logistic.Repositories.Interfaces
{
    public interface IInMemoryRepository<TEntity>
        where TEntity : class, IEntity
    {
        void Create(TEntity entity);

        List<TEntity> ReadAll();

        TEntity Read(int id);

        void Update(int id, TEntity updateTo);

        void Delete(int id);
    }
}