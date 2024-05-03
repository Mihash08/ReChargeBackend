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
    public class LocationRepository : AbstractRepository, ILocationRepository
    {
        readonly DbSet<Location> dbSet;
        public LocationRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<Location>();
        }


        public async Task<Location> AddAsync(Location entity)
        {
            await dbSet.AddAsync(entity);
            context.SaveChanges();
            return entity;
        }

        public async Task DeleteAsync(Location entity)
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

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            var entity = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Id not found", nameof(id));
            }
            return entity;
        }

        public async Task<Location> UpdateAsync(Location entity)
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
            existingEntity.AddressBuildingNumber = entity.AddressBuildingNumber;
            existingEntity.AddressCity = entity.AddressCity;
            existingEntity.AddressLongitude = entity.AddressLongitude;
            existingEntity.AddressLatitude = entity.AddressLatitude;
            existingEntity.AdminWA = entity.AdminWA;
            existingEntity.AdminTG = entity.AdminTG;
            existingEntity.AddressStreet = entity.AddressStreet;
            existingEntity.Image = entity.Image;
            existingEntity.LocationDescription = entity.LocationDescription;
            existingEntity.LocationName = entity.LocationName;
            return existingEntity;
        }
    }
}
