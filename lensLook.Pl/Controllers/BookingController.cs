using AutoMapper;
using lensLook.Bll;
using lensLook.Bll.Services;
using lensLook.Dal.Models;
using lensLook.Pl.Models.BookingViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace lensLook.Pl.Controllers
{
    public class BookingController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServicesRepo _servicesRepo;
        private readonly IRequestServices _requestService;

        public BookingController(IMapper mapper, IServicesRepo servicesRepo, IRequestServices requestService)
        {
            _mapper = mapper;
            _servicesRepo = servicesRepo;
            _requestService = requestService;
        }

        [HttpGet]
        public IActionResult Repair()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Repair(BookingCreateVm model)
        {
            return HandleBooking(model, "repairs");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Online()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Online(BookingCreateVm model)
        {
            return HandleBooking(model, "Online");
        }

        [HttpGet]
        public IActionResult Offline()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Offline(BookingCreateVm model)
        {
            return HandleBooking(model, "HomeVisit");
        }

        public IActionResult AllBookingForUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var allRequests = _requestService.GetServicesByUser(userId);
            return View(allRequests);
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult Appointment()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var allRequests = _requestService.GetAllForDoctor(userId);
            return View(allRequests);
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult AcceptRequest(int id)
        {
            return UpdateRequestStatus(id, BookingStatus.Accepted);
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult RejectRequest(int id)
        {
            return UpdateRequestStatus(id, BookingStatus.Rejected);
        }




        private IActionResult HandleBooking(BookingCreateVm model, string serviceName)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                var booking = _mapper.Map<Booking>(model);
                booking.UserId = userId;
                var serviceType = _servicesRepo.GetByName(serviceName);
                booking.ServiceId = serviceType.Id;

                var result = _requestService.Create(booking);
                if (!result)
                {
                    return View(model);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        private IActionResult UpdateRequestStatus(int id, BookingStatus status)
        {
            var request = _requestService.GetById(id);
            if (request == null)
            {
                return NotFound();
            }

            request.Status = status;
            request.DoctorStatus = status;
            request.DoctorResponseTime = DateTime.Now;
            request.AdminStatus = status;

            _requestService.Update(request);
            return RedirectToAction(nameof(Appointment));
        }
    }
}
