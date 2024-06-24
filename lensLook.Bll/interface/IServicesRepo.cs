
namespace lensLook.Bll
{
    public interface IServicesRepo
    {
        bool Create(lensLook.Dal.Models.Services model);

        List<lensLook.Dal.Models.Services> GetAll();

        lensLook.Dal.Models.Services GetById(int id);
        lensLook.Dal.Models.Services GetByName(string id);




    }
}
