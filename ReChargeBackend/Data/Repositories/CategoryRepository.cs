using Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using SportsStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CategoryRepository : AbstractRepository, ICategoryRepository
    {
        readonly DbSet<Category> dbSet;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<Category>();
        }


        public Category Add(Category entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(Category entity)
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

        public IEnumerable<Category> GetAll()
        {
            return dbSet.ToList();
        }

        public Category? GetById(int id)
        {
            var entity = dbSet.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Id not found", nameof(id));
            }
            return entity;
        }

        public Category Update(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity not found");
            }
            var existingEntity = dbSet.FirstOrDefault(x => x.Id == entity.Id);
            if (existingEntity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity not found");
            }
            existingEntity.Id = entity.Id;
            existingEntity.Image = entity.Image;
            existingEntity.Name = entity.Name;
            return existingEntity;
        }
    }
}
