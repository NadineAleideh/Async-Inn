namespace Async_Inn.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Layout layout { get; set; }


        //Navigation Properties 
        public List<RoomAmenity> RoomAmenities { get; set; }
        public List<HotelRoom> HotelRooms { get; set; }
    }

    public enum Layout
    {
        Studio,
        OneBedroom,
        TwoBedroom
    }

}

