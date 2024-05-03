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

namespace Data.Repositories
{
    public class VerificationCodeRepository : AbstractRepository, IVerificationCodeRepository
    {
        readonly DbSet<VerificationCode> dbSet;
        public VerificationCodeRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<VerificationCode>();
        }


        public async Task<VerificationCode> AddAsync(VerificationCode entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(VerificationCode entity)
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

        public async Task<IEnumerable<VerificationCode>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<VerificationCode> GetByIdAsync(int id)
        {
            var entity = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Id not found", nameof(id));
            }
            return entity;
        }

        public async Task<VerificationCode> GetBySessionAsync(string sessionId)
        {
            var entity = await dbSet.FirstOrDefaultAsync(x => x.SessionId == sessionId);
            if (entity == null)
            {
                throw new ArgumentException("Session not found");
            }
            return entity;
        }

        public async Task<VerificationCode> UpdateAsync(VerificationCode entity)
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
            existingEntity.Code = entity.Code;
            existingEntity.PhoneNumber = entity.PhoneNumber;
            existingEntity.SessionId = entity.SessionId;
            //todo: add saveChanges everywhere
            await context.SaveChangesAsync();
            return existingEntity;
        }
    }
}
