using Data.Entities;

namespace Data.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        public Task<Reservation?> GetNextReservationAsync(int userId);
        public Task<IEnumerable<Reservation>> GetReservationsByUserAsync(int userId);
        public Task<IEnumerable<Reservation>> GetReservationsByLocationAsync(int locationId);
        public Task<Reservation?> GetReservationByCodeAsync(string code);
    }
}
