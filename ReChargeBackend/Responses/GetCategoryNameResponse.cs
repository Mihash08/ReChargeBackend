namespace ReChargeBackend.Responses
{
    public class GetSlotsByCategoryAndDateResponse
    {
        public List<GetSlotByCategoryAndDateResponse> Slots { get; set; }
        public string CategoryName {  get; set; }
        public DateTime dateTime {  get; set; }
    }
}
