namespace Async_Inn.Models
{
    public class RoomAmenity
    {
        public int AmenityId { get; set; }
        public int RoomId { get; set; }


        //Navigation Properties
        public Amenity Amenities { get; set; }
        public Room Room { get; set; }
    }
}
