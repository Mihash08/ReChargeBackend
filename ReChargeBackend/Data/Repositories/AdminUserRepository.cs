using Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using ReCharge.Data;
using ReCharge.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using System.Threading.Tasks;
using Utility;

namespace Data.Repositories
{
    public class AdminUserRepository : AbstractRepository, IAdminUserRepository
    {
        readonly DbSet<AdminUser> dbSet;
        public AdminUserRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<AdminUser>();
        }


        public AdminUser Add(AdminUser entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(AdminUser entity)
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

        public IEnumerable<AdminUser> GetAll()
        {
            return dbSet.Include(x => x.Reservations).ToList();
        }

        public AdminUser? GetByAccessToken(string accessToken)
        {
            try
            {
                var test = Hasher.Verify("123", "$2a$11$zf7y0J4heYz1ufhpxI71du$2a$11$zf7y0J4heYz1ufhpxI71duIZhDleVZZ2eiOpURfQiBVMcHDvVKYP2");
                var entity = dbSet.Include(x => x.Reservations).ToList().First(x => 
                    x.AccessHash != null && Hasher.Verify(accessToken, x.AccessHash));
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public AdminUser? GetById(int id)
        {
            try
            {
                var entity = dbSet.Include(x => x.Reservations).First(x => x.Id == id);
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public AdminUser? GetByNumber(string number)
        {
            var entity = dbSet.Include(x => x.Reservations).FirstOrDefault(x => x.PhoneNumber == number);
            return entity;
        }
        public AdminUser Update(AdminUser entity)
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
            existingEntity.Email = entity.Email;
            existingEntity.Name = entity.Name;
            existingEntity.PhoneNumber = entity.PhoneNumber;
            existingEntity.Surname = entity.Surname;    
            existingEntity.AccessHash = entity.AccessHash;
            context.SaveChanges();
            return existingEntity;
        }
    }
}
