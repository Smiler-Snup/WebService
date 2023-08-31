using WebService.Model;

namespace WebService.DataAccessLayer.Interfaces
{
    public interface IAccessPassport
    {
        public Passport FindById(int id);
    }
}
