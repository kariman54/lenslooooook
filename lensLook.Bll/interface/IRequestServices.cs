using lensLook.Dal.Models;

namespace lensLook.Bll
{
    public interface IRequestServices
    {
        public bool Create(Booking model);
        public bool Update(Booking model);
        public List<Booking> GetAll();
        public Booking GetById(int Id);
        public bool ChangeStatus(int Id, string Status);

        List<lensLook.Dal.Models.Booking> GetServicesByUser(string IdUser);
        List<Booking> GetAllForDoctor(string Id);

        int TotalServices();

        int TotalServicesPending();
        int TotalServicesSuccess();



        bool Delete(int ModelBookingId);


    }
}
