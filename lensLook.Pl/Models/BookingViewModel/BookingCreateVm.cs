using lensLook.Dal.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace lensLook.Pl.Models.BookingViewModel
{
    public class BookingCreateVm
    {

        public int ServiceId { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? HomeAddres { get; set; }
        public DateTime RequestDate { get; set; }= DateTime.Now;
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
        public BookingStatus Status { get; set; }=BookingStatus.Pending;



        public BookingStatus AdminStatus { get; set; } = BookingStatus.Pending;
        public BookingStatus DoctorStatus { get; set; } = BookingStatus.Pending;
        public DateTime? AdminReceivedTime { get; set; }=null;
        public DateTime? DoctorResponseTime { get; set; } = null;


    }
}
