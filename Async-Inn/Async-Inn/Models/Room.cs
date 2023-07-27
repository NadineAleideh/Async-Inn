namespace Async_Inn.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }


        //Navigation Properties 
        public List<HotelRoom> HotelRoom { get; set; }
        public List<RoomAmenity> RoomAmenities { get; set; }
    }
}

