using AutoMapper;
using lensLook.Dal.Models;
using lensLook.Pl.Models.BookingViewModel;

namespace lensLook.Pl.Helper
{
    public class maping:Profile
    {
        public maping()
        {
            CreateMap<BookingCreateVm, Booking>().ReverseMap();
        }
    }
}
