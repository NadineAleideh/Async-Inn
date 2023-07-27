namespace Async_Inn.Models
{
    public class Amenity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation Properties
        public List<RoomAmenity> RoomAmenities { get; set; }

    }
}
