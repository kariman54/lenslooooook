using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lensLook.Dal.Models
{
    public class Booking
    {
    
            [Key]
            public int BookingId { get; set; }
            public string UserId { get; set; }
            public string? AdminId { get; set; }
            public string? DoctorId { get; set; }

            public user User { get; set; }
            public user? Admin { get; set; }
            public user? Doctor { get; set; }















            [ForeignKey("Services")]
            public int ServiceId { get; set; }
            public Services Services { get; set; }




            public string FullName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string? HomeAddres { get; set; }
            public DateTime RequestDate { get; set; }
            public DateTime AppointmentDate { get; set; }
            public string Reason { get; set; }
            public BookingStatus Status { get; set; }



            public BookingStatus AdminStatus { get; set; }
            public BookingStatus DoctorStatus { get; set; }
            public DateTime? AdminReceivedTime { get; set; }
            public DateTime? DoctorResponseTime { get; set; }
        }

        public enum BookingStatus
        {
            Pending,
            Accepted,
            Rejected
        }
    
}
