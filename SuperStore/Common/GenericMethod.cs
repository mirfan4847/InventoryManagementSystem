using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.Models;
using System.Data.Entity;

namespace SuperStore.Common
{
    public abstract class GenericMethod<TEntity>  where TEntity : class
    {
        protected readonly ApplicationDbContext _Context;
        protected readonly DbSet<TEntity> DbSet;


        public GenericMethod(ApplicationDbContext context)
        {
            _Context = context;
            this.DbSet = context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            SuperStoreEntities _db = new SuperStoreEntities();
            return  _db.Set<TEntity>().Find(id);
        }
    }
}