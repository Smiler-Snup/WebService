using Dapper;
using Microsoft.Data.SqlClient;
using WebService.DataAccessLayer.Interfaces;
using WebService.Model;

namespace WebService.DataAccessLayer.Implementations
{
    public class AccessDepartment : IAccessDepartment
    {
        /// <summary>
        /// Возвращает объект отдела по уникальному идентификатору
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Department FindById(int Id)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();

                var SqlCode = @"SELECT * FROM Department where Id=@id";

                var department = connection.QuerySingleOrDefault<Department>(SqlCode, new { Id });

                connection.Close();

                return department;
            }
        }

        /// <summary>
        /// Возвращает объект отдела по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Department FindByName(string name)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();

                var SqlCode = @"SELECT * FROM Department where Name=@name";

                var department = connection.QuerySingleOrDefault<Department>(SqlCode, new { name });

                connection.Close();

                return department;
            }
        }
    }
}
