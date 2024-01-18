using Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using SportsStore.Data;
using SportsStore.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : AbstractRepository, IUserRepository
    {
        readonly DbSet<User> dbSet;
        public UserRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<User>();
        }


        public User Add(User entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(User entity)
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

        public IEnumerable<User> GetAll()
        {
            return dbSet.ToList();
        }

        public User GetById(int id)
        {
            var entity = dbSet.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Id not found", nameof(id));
            }
            return entity;
        }
        public User GetByNumber(string number)
        {
            var entity = dbSet.FirstOrDefault(x => x.PhoneNumber == number);
            if (entity == null)
            {
                throw new ArgumentException("Number not found", nameof(number));
            }
            return entity;
        }
        public User Update(User entity)
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
            existingEntity.BirthDate = entity.BirthDate;
            existingEntity.Email = entity.Email;
            existingEntity.Name = entity.Name;
            existingEntity.PhoneNumber = entity.PhoneNumber;
            existingEntity.Reservations = entity.Reservations;
            existingEntity.Surname = entity.Surname;
            return existingEntity;
        }
    }
}
