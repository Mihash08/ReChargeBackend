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
    public class UserRepository : AbstractRepository, IUserRepository
    {
        readonly DbSet<User> dbSet;
        public UserRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<User>();
        }


        public async Task<User> AddAsync(User entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(User entity)
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

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbSet.Include(x => x.Reservations).ToListAsync();
        }

        public async Task<User?> GetByAccessTokenAsync(string accessToken)
        {
            try
            {
                var test = Hasher.Verify("123", "$2a$11$zf7y0J4heYz1ufhpxI71du$2a$11$zf7y0J4heYz1ufhpxI71duIZhDleVZZ2eiOpURfQiBVMcHDvVKYP2");
                var entity = (await dbSet.Include(x => x.Reservations).ToListAsync()).First(x => 
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

        public async Task<User?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await dbSet.Include(x => x.Reservations).FirstAsync(x => x.Id == id);
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<User?> GetByNumberAsync(string number)
        {
            var entity = await dbSet.Include(x => x.Reservations).FirstOrDefaultAsync(x => x.PhoneNumber == number);
            return entity;
        }
        public async Task<User> UpdateAsync(User entity)
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
            existingEntity.BirthDate = entity.BirthDate;
            existingEntity.Email = entity.Email;
            existingEntity.City = entity.City;
            existingEntity.Name = entity.Name;
            existingEntity.PhoneNumber = entity.PhoneNumber;
            existingEntity.ImageUrl = entity.ImageUrl;
            existingEntity.Surname = entity.Surname;    
            existingEntity.Gender = entity.Gender;
            existingEntity.AccessHash = entity.AccessHash;
            await context.SaveChangesAsync();
            return existingEntity;
        }
    }
}
