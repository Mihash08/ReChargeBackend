
namespace ReChargeBackend.Responses
{
    public class GetActivityViewSlotsResponse
    {
        public SlotView[] Slots;
        public DateTime DateTime;

    }

    public class SlotView
    {
        public int SlotId { get; set; }
        public double Price { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }
    }
}