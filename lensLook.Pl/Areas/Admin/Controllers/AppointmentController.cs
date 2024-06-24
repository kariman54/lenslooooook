using lensLook.Bll;
using lensLook.Dal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lensLook.Pl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AppointmentController : Controller
    {
        private readonly IRequestServices requestsServices;

        public AppointmentController(IRequestServices RequestsServices)
        {
            requestsServices = RequestsServices;
        }
        public IActionResult Index()
        {
            var AllRequests = requestsServices.GetAll();
            return View(AllRequests);
        }


        public IActionResult DeleteBooking(int id)
        {
            requestsServices.Delete(id);
            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public IActionResult ChooseDoctorForAppointment(int id)
        {
            var Booking = requestsServices.GetById(id);
            return View(Booking);

        }



        [HttpPost]
        public IActionResult ChooseDoctorForAppointment(Booking Model)
        {
            var BookingOld = requestsServices.GetById(Model.BookingId);

            BookingOld.DoctorId=Model.DoctorId;
            BookingOld.Status=BookingStatus.Pending;
            BookingOld.DoctorStatus=BookingStatus.Pending;
            BookingOld.AdminStatus=BookingStatus.Pending;
            requestsServices.Update(BookingOld);
            return RedirectToAction(nameof(Index));

        }


    }
}
