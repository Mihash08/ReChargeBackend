using Data.Entities;

namespace Data.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        public Reservation? GetNextReservation(int userId);
        public IEnumerable<Reservation> GetReservationsByUser(int userId);
    }
}
