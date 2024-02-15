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
    public class SlotRepository : AbstractRepository, ISlotRepository
    {
        readonly DbSet<Slot> dbSet;
        public SlotRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<Slot>();
        }


        public Slot Add(Slot entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(Slot entity)
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

        public IEnumerable<Slot> GetAll()
        {
            return dbSet.ToList();
        }

        public IEnumerable<Slot> GetAllByActivityId(int activityId)
        {
            return dbSet.Where(x => x.ActivityId == activityId).ToList();
        }

        public Slot? GetById(int id)
        {
            var entity = dbSet.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        public Slot Update(Slot entity)
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
            existingEntity.Activity = entity.Activity;
            existingEntity.ActivityId = entity.ActivityId;
            existingEntity.FreePlaces = entity.FreePlaces;
            existingEntity.Price = entity.Price;
            existingEntity.SlotDateTime = entity.SlotDateTime;
            return existingEntity;
        }
    }
}
