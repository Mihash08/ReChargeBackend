using Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using ReCharge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CategoryCategoryRepository : AbstractRepository, ICategoryCategoryRepository
    {
        readonly DbSet<CategoryCategory> dbSet;
        public CategoryCategoryRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<CategoryCategory>();
        }


        public CategoryCategory Add(CategoryCategory entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(CategoryCategory entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity was null");
            }
            if (dbSet.Contains(entity))
            {
                dbSet.Remove(entity);
                context.SaveChanges();
            }

        }

        public void DeleteById(int id)
        {
            var entity = dbSet.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Id not found", nameof(id));
            }
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<CategoryCategory> GetAll()
        {
            return dbSet.ToList();
        }

        public CategoryCategory? GetById(int id)
        {
            var entity = dbSet.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Id not found", nameof(id));
            }
            return entity;
        }

        public CategoryCategory Update(CategoryCategory entity)
        {
            throw new NotImplementedException();
        }
    }
}
