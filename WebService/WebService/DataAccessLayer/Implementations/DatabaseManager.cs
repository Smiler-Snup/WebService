using WebService.DataAccessLayer.Interfaces;

namespace WebService.DataAccessLayer.Implementations
{
    /// <summary>
    /// Класс собирает всю логику по взаимодействию с базой данных
    /// </summary>
    public class DatabaseManager
    {
        public readonly IAccessCompany ImplementationAccessCompany;
        public readonly IAccessDepartment ImplementationAccessDepartment;
        public readonly IAccessEmployee ImplementationAccessEmployee;
        public readonly IAccessPassport ImplementationAccessPassport;
        public DatabaseManager(IAccessCompany implementationAccessCompany,
            IAccessDepartment implementationAccessDepartment,
            IAccessEmployee implementationAccessEmployee,
            IAccessPassport implementationAccessPassport)
        {
            ImplementationAccessCompany = implementationAccessCompany;
            ImplementationAccessDepartment = implementationAccessDepartment;
            ImplementationAccessEmployee = implementationAccessEmployee;
            ImplementationAccessPassport = implementationAccessPassport;
        }

    }
}
