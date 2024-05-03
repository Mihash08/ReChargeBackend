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
    public class ActivityRepository : AbstractRepository, IActivityRepository
    {
        readonly DbSet<Activity> dbSet;
        public ActivityRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<Activity>();
        }


        public async Task<Activity> AddAsync(Activity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Activity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity was null");
            }
            if (dbSet.Contains(entity))
            {
                dbSet.Remove(entity);
                await context.SaveChangesAsync();
            }

        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Id not found", nameof(id));
            }
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Activity>> GetAllAsync()
        {
            return await dbSet.Include(x => x.Slots).ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetByCategoryAsync(int categoryId)
        {
            if (categoryId == -1)
            {
                return await dbSet.Include(x => x.Slots).Include(x => x.Category).Include(x => x.Location).ToListAsync();
            }
            return await dbSet.Where(x => x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Slots).Include(x => x.Location).ToListAsync();
        }

        public async Task<Activity?> GetByIdAsync(int id)
        {
            var entity = await dbSet.Include(x => x.Slots).Include(x => x.Location)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        public async Task<Activity> UpdateAsync(Activity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity not found");
            }
            var existingEntity = await dbSet.Include(x => x.Slots).FirstOrDefaultAsync(x => x.Id == entity.Id);
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
