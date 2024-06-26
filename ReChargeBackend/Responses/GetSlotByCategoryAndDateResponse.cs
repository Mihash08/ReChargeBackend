﻿using ReChargeBackend.Utility;

namespace ReChargeBackend.Responses
{
    public class GetSlotByCategoryAndDateResponse
    {
        public int SlotId { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName {  get; set; }
        public string ImageUrl {  get; set; }
        public string CategoryName {  get; set; }
        public DateTime DateTime { get; set; }
        public int LengthMinutes { get; set; }
        public int Price {  get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}
