using lensLook.Dal.Context;
using lensLook.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lensLook.Bll.Services
{
    
    public class RequestServicesRepo : IRequestServices
    {
        private readonly LensLookDbContext _Context;

        public RequestServicesRepo(LensLookDbContext context)
        {
            _Context = context;
        }
        public bool ChangeStatus(int Id, string Status)
        {
            var OldRequest = GetById(Id);

            if (OldRequest == null)
            {
                return false; // or handle the case where the entity with Id is not found
            }

            // Assuming Status is of type bookingStatus enum
            if (Enum.TryParse(Status, out BookingStatus newStatus))
            {
                OldRequest.Status = newStatus;

                // Update entity in DbSet
                _Context.Bookings.Update(OldRequest);

                // Save changes to database
                _Context.SaveChanges();

                return true; // Return true to indicate success
            }
            else
            {
                return false; // Handle case where Status string cannot be parsed to enum
            }
        }

        public bool Create(Booking model)
        {
            _Context.Bookings.Add(model);
            _Context.SaveChanges();
            return true;
        }

        public List<Booking> GetAll()
        {
            return _Context.Bookings.OrderBy(x=>x.RequestDate).ToList();
        }

        public List<Booking> GetAllForDoctor(string Id)
        {
            return _Context.Bookings.Where(x=>x.DoctorId== Id&&x.DoctorStatus==BookingStatus.Pending).ToList();
        }

        public Booking GetById(int Id)
        {
          return  _Context.Bookings.FirstOrDefault(x=> x.BookingId== Id);
        }

        public List<Dal.Models.Booking> GetServicesByUser(string IdUser)
        {
           return _Context.Bookings.Where(x=>x.UserId==IdUser).ToList();
        }

        public bool Update(Booking model)
        {
            try
            {
                var res = _Context.Bookings.Update(model);
                var res2 = _Context.SaveChanges();
            return true;
            }
            catch (Exception)
            {
                return false;

                throw;
            }

        }










        public int TotalServices()
        {
       return     _Context.Bookings.Count();
        }
         
        public int TotalServicesPending(){
        return _Context.Bookings.Where(x=>x.Status==BookingStatus.Pending).Count();
        }
        public int TotalServicesSuccess() {

            return _Context.Bookings.Where(x => x.Status == BookingStatus.Accepted).Count();
        }

        public bool Delete(int ModelBookingId)
        {
            try
            {
                var Booking = _Context.Bookings.FirstOrDefault(x => x.BookingId == ModelBookingId);
                _Context.Bookings.Remove(Booking);
                _Context.SaveChanges(true);
                return true;
            }
            catch (Exception)
            {

                return false;
                throw;
            }
            
         }
    }
}
