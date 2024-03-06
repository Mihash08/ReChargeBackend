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
    public class VerificationCodeRepository : AbstractRepository, IVerificationCodeRepository
    {
        readonly DbSet<VerificationCode> dbSet;
        public VerificationCodeRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<VerificationCode>();
        }


        public VerificationCode Add(VerificationCode entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(VerificationCode entity)
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

        public IEnumerable<VerificationCode> GetAll()
        {
            return dbSet.ToList();
        }

        public VerificationCode GetById(int id)
        {
            var entity = dbSet.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Id not found", nameof(id));
            }
            return entity;
        }

        public VerificationCode GetBySession(string sessionId)
        {
            var entity = dbSet.FirstOrDefault(x => x.SessionId == sessionId);
            if (entity == null)
            {
                throw new ArgumentException("Session not found");
            }
            return entity;
        }

        public VerificationCode Update(VerificationCode entity)
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
            existingEntity.Code = entity.Code;
            existingEntity.PhoneNumber = entity.PhoneNumber;
            existingEntity.SessionId = entity.SessionId;
            //todo: add saveChanges everywhere
            context.SaveChanges();
            return existingEntity;
        }
    }
}
