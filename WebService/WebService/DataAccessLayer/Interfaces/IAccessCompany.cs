using WebService.Model;

namespace WebService.DataAccessLayer.Interfaces
{
    public interface IAccessCompany
    {
        public Company FindByName(string name);
        public Company FindById(int Id);
    }
}
