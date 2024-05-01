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


        public Reservation Add(Reservation entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(Reservation entity)
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
            var entity = dbSet.Include(x => x.User).Include(x => x.Slot).FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Id not found", nameof(id));
            }
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<Reservation> GetAll()
        {
            return dbSet.Include(x => x.User).Include(x => x.Slot).ToList();
        }

        public Reservation GetById(int id)
        {
            var entity = dbSet.Include(x => x.User).Include(x => x.Slot).FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        public Reservation Update(Reservation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity not found");
            }
            var existingEntity = dbSet.Include(x => x.User).Include(x => x.Slot).FirstOrDefault(x => x.Id == entity.Id);
            if (existingEntity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity not found");
            }
            existingEntity.Slot = entity.Slot;
            existingEntity.SlotId = entity.SlotId;
            existingEntity.User = entity.User;
            existingEntity.UserId = entity.UserId;
            existingEntity.Status = entity.Status;
            context.SaveChanges();
            return existingEntity;
        }


        public Reservation? GetNextReservation(int userId)
        {
            var reservations = dbSet.Include(x => x.Slot.Activity.Location)
                .Where(x => x.UserId == userId)
                .Where(x => x.Slot.SlotDateTime + new TimeSpan(0, x.Slot.LengthMinutes, 0) >= DateTime.Now)
                .Where(x => x.Status == Status.New || x.Status == Status.Confirmed);
            if (reservations == null || reservations.Count() < 1)
            {
                return null;
            }
            return reservations.OrderBy(x => x.Slot.SlotDateTime).ToList()[0];
        }

        public IEnumerable<Reservation> GetReservationsByUser(int userId)
        {
            var reservations = dbSet.Include(x => x.Slot.Activity.Location)
                .Where(x => x.Slot.SlotDateTime >= DateTime.Now)
                .Where(x => x.UserId == userId);
            if (reservations == null)
            {
                return null;
            }
            return reservations.ToList();
        }

        public Reservation? GetReservationByCode(string code)
        {
            var reservation = dbSet.Where(x => x.AccessCode == code).Include(x => x.Slot.Activity);
            if (reservation.Count() > 0)
            {
                return reservation.First();
            }
            return null;
        }

        public IEnumerable<Reservation> GetReservationsByLocation(int locationId)
        {
            return dbSet.Where(x => x.Slot.Activity.LocationId == locationId).Include(x => x.Slot);
        }
    }
}
