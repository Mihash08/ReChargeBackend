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
    public class ReservationRepository : AbstractRepository, IReservationRepository
    {
        readonly DbSet<Reservation> dbSet;
        public ReservationRepository(AppDbContext context) : base(context)
        {
            dbSet = context.Set<Reservation>();
        }


        public async Task<Reservation> AddAsync(Reservation entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            Console.WriteLine("added");
            Console.WriteLine("added");
            Console.WriteLine("added");
            Console.WriteLine("added");
            Console.WriteLine("added");
            return entity;
        }

        public async Task DeleteAsync(Reservation entity)
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
            var entity = await dbSet.Include(x => x.User).Include(x => x.Slot).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Id not found", nameof(id));
            }
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await dbSet.Include(x => x.User).Include(x => x.Slot).ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            var entity = await dbSet.Include(x => x.User).Include(x => x.Slot.Activity.Location).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        public async Task<Reservation> UpdateAsync(Reservation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity not found");
            }
            var existingEntity = await dbSet.Include(x => x.User).Include(x => x.Slot).FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (existingEntity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity not found");
            }
            existingEntity.Slot = entity.Slot;
            existingEntity.SlotId = entity.SlotId;
            existingEntity.User = entity.User;
            existingEntity.UserId = entity.UserId;
            existingEntity.Status = entity.Status;
            await context.SaveChangesAsync();
            return existingEntity;
        }


        public async Task<Reservation?> GetNextReservationAsync(int userId)
        {
            var reservations = dbSet.Include(x => x.Slot.Activity.Location)
                .Where(x => x.UserId == userId)
                .Where(x => x.Slot.SlotDateTime.AddMinutes(x.Slot.LengthMinutes) >= DateTime.Now)
                .Where(x => x.Status == Status.New || x.Status == Status.Confirmed);
            if (reservations == null || reservations.Count() < 1)
            {
                return null;
            }
            return (await reservations.OrderBy(x => x.Slot.SlotDateTime).ToListAsync())[0];
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserAsync(int userId)
        {
            var reservations = dbSet.Include(x => x.Slot.Activity.Location)
                .Where(x => x.Slot.SlotDateTime >= DateTime.Now)
                .Where(x => x.UserId == userId);
            if (reservations == null)
            {
                return null;
            }
            return await reservations.ToListAsync();
        }

        public async Task<Reservation?> GetReservationByCodeAsync(string code)
        {
            var reservation = dbSet.Where(x => x.AccessCode == code).Include(x => x.Slot.Activity);
            if (reservation.Count() > 0)
            {
                return await reservation.FirstAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByLocationAsync(int locationId)
        {
            return await dbSet.Where(x => x.Slot.Activity.LocationId == locationId).Include(x => x.Slot).ToListAsync();
        }
    }
}
