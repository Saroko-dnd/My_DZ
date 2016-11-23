﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        void Add(TEntity NewEntity);
        void Update(TEntity ModifiedEntity);
    }
}
