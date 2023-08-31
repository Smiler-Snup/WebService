using WebService.Model;

namespace WebService.DataAccessLayer.Interfaces
{
    public interface IAccessDepartment
    {
        public Department FindByName(string name);
        public Department FindById(int Id);
    }
}
