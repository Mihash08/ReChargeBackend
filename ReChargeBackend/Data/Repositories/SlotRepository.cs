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
using static System.Reflection.Metadata.BlobBuilder;

namespace Data.Repositories
{
    public class SlotRepository : AbstractRepository, ISlotRepository
    {
        readonly DbSet<Slot> dbSet;
        public SlotRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<Slot>();
        }


        public async Task<Slot> AddAsync(Slot entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Slot entity)
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
            var entity = dbSet.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Id not found", nameof(id));
            }
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Slot>> GetAllAsync()
        {
            return await dbSet.Include(x => x.Activity).ToListAsync();
        }

        public async Task<IEnumerable<Slot>> GetAllByActivityIdAsync(int activityId)
        {
            return await dbSet.Include(x => x.Activity).Where(x => x.ActivityId == activityId).ToListAsync();
        }

        public async Task<Slot?> GetByIdAsync(int id)
        {
            var entity = await dbSet.Include(x => x.Activity).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        public async Task<IEnumerable<Slot>> GetSlotsByActivityIdAndTimeAsync(int activityId, DateTime dateTime)
        {
            return await dbSet.Where(x => x.ActivityId == activityId && x.SlotDateTime > dateTime && x.SlotDateTime.Date < dateTime.AddHours(24))
                .Include(x => x.Activity.Location).ToListAsync();
        }

        public async Task<IEnumerable<Slot>> GetSlotsByCategoryIdAndTimeAsync(int categoryId, DateTime dateTime)
        {
            return await dbSet
                .Where(
                    x => x.Activity.CategoryId == categoryId 
                    && x.SlotDateTime > dateTime 
                    && x.SlotDateTime.Date < dateTime.AddHours(24)
                    && x.SlotDateTime > DateTime.Now
                )
                .Include(x => x.Activity.Location).Include(x => x.Activity.Category).ToListAsync();
        }

        public async Task<Slot> UpdateAsync(Slot entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity not found");
            }
            var existingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (existingEntity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity not found");
            }
            existingEntity.Activity = entity.Activity;
            existingEntity.ActivityId = entity.ActivityId;
            existingEntity.FreePlaces = entity.FreePlaces;
            existingEntity.Price = entity.Price;
            existingEntity.SlotDateTime = entity.SlotDateTime;
            return existingEntity;
        }
    }
}
