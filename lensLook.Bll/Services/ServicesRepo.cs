using lensLook.Dal.Context;
using lensLook.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lensLook.Bll.Services
{
    public class ServicesRepo : IServicesRepo
    {
        private readonly LensLookDbContext _Context;

        public ServicesRepo(LensLookDbContext context)
        {
            _Context = context;
        }
        public bool Create(Dal.Models.Services model)
        {
            _Context.Services.Add(model);
            return true;
        }

        public List<Dal.Models.Services> GetAll()
        {
          return _Context.Services.ToList();
        }

        public Dal.Models.Services GetById(int id)
        {
         return   _Context.Services.FirstOrDefault(s => s.Id == id);
        }


        public Dal.Models.Services GetByName(string Name)
        {
            // Convert the input string to a BookingType enum
            if (Enum.TryParse(typeof(BookingType), Name.ToLower(), true, out var bookingType))
            {
                // Cast the result to BookingType and use it in the query
                return _Context.Services.FirstOrDefault(x => x.BookingType == (BookingType)bookingType);
            }
            // Return null if the conversion failed
            return null;
        }


    }
}
