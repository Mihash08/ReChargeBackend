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


        public Location Add(Location entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(Location entity)
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

        public IEnumerable<Location> GetAll()
        {
            return dbSet.ToList();
        }

        public Location GetById(int id)
        {
            var entity = dbSet.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Id not found", nameof(id));
            }
            return entity;
        }

        public Location Update(Location entity)
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
