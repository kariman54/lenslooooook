

namespace lensLook.Dal.Models
{
    public class Services
    {

        public int Id { get; set; }
        public BookingType BookingType { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
    public enum BookingType
    {
        HomeVisit,
        Online,
        Repairs
    }

}

