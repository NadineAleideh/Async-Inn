using System.ComponentModel.DataAnnotations.Schema;

namespace Async_Inn.Models
{
    public class HotelRoom
    {
        public int HotelId { get; set; }
        public int RoomNumber { get; set; }
        public int RoomId { get; set; }
        public double Price { get; set; }
        public bool PetFriendly { get; set; }


        //Navigation Properties
        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; set; }

        [ForeignKey("RoomId")]
        public Room? Room { get; set; }
    }
}
