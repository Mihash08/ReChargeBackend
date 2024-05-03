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


        public async Task<AdminUser> AddAsync(AdminUser entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(AdminUser entity)
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

        public async Task<IEnumerable<AdminUser>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<AdminUser?> GetByAccessTokenAsync(string accessToken)
        {
            try
            {
                var test = Hasher.Verify("123", "$2a$11$zf7y0J4heYz1ufhpxI71du$2a$11$zf7y0J4heYz1ufhpxI71duIZhDleVZZ2eiOpURfQiBVMcHDvVKYP2");
                var entity = (await dbSet.ToListAsync()).First(x =>
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

        public async Task<AdminUser?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await dbSet.FirstAsync(x => x.Id == id);
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<AdminUser?> GetByNumberAsync(string number)
        {
            var entity = await dbSet.FirstOrDefaultAsync(x => x.PhoneNumber == number);
            return entity;
        }
        public async Task<AdminUser> UpdateAsync(AdminUser entity)
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
            existingEntity.Email = entity.Email;
            existingEntity.Name = entity.Name;
            existingEntity.PhoneNumber = entity.PhoneNumber;
            existingEntity.Surname = entity.Surname;    
            existingEntity.AccessHash = entity.AccessHash;
            await context.SaveChangesAsync();
            return existingEntity;
        }
    }
}
