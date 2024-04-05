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
    public class ActivityRepository : AbstractRepository, IActivityRepository
    {
        readonly DbSet<Activity> dbSet;
        public ActivityRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<Activity>();
        }


        public Activity Add(Activity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(Activity entity)
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

        public IEnumerable<Activity> GetAll()
        {
            return dbSet.Include(x => x.Slots).ToList();
        }

        public IEnumerable<Activity> GetByCategory(int categoryId)
        {
            if (categoryId == -1)
            {
                return dbSet.Include(x => x.Slots).Include(x => x.Category).ToList();
            }
            return dbSet.Where(x => x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Slots).ToList();
        }

        public Activity? GetById(int id)
        {
            var entity = dbSet.Include(x => x.Slots).FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        public Activity Update(Activity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity not found");
            }
            var existingEntity = dbSet.Include(x => x.Slots).FirstOrDefault(x => x.Id == entity.Id);
            if (existingEntity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity not found");
            }
            existingEntity.Location = entity.Location;
            existingEntity.LocationId = entity.LocationId;
            existingEntity.ActivityDescription = entity.ActivityDescription;
            existingEntity.ActivityName = entity.ActivityName;
            existingEntity.ActivityAdminWa = entity.ActivityAdminWa;    
            existingEntity.ActivityAdminTg = entity.ActivityAdminTg;
            existingEntity.ActivityDescription = entity.ActivityDescription;
            existingEntity.Category = entity.Category;
            existingEntity.CategoryId = entity.CategoryId;
            existingEntity.ShouldDisplayWarning = entity.ShouldDisplayWarning;
            existingEntity.WarningText = entity.WarningText;
            return existingEntity;
        }
    }
}
