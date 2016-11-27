using RaceInfrastructure;
using RaceInfrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceDataAccess.Repositories
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private RaceApplicationDataContext CurrentDataContext;
        private DbSet<TEntity> set;

        internal Repository(RaceApplicationDataContext NewDataContext)
        {
            CurrentDataContext = NewDataContext;
        }

        protected DbSet<TEntity> Set
        {
            get
            {
                return set ?? (set = CurrentDataContext.Set<TEntity>());
            }
        }

        public void Add(TEntity NewEntity)
        {
            Set.Add(NewEntity);
        }

        public void Delete(TEntity EntityToBeDeleted)
        {
            Set.Remove(EntityToBeDeleted);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Set;
        }

        public void Update(TEntity ModifiedEntity)
        {
            var Entry = CurrentDataContext.Entry(ModifiedEntity);
            if (Entry.State == EntityState.Detached)
            {
                Set.Attach(ModifiedEntity);
                Entry = CurrentDataContext.Entry(ModifiedEntity);
            }
            Entry.State = EntityState.Modified;
        }
    }
}
