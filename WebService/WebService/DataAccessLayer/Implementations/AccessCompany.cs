using Dapper;
using Microsoft.Data.SqlClient;
using WebService.DataAccessLayer.Interfaces;
using WebService.Model;

namespace WebService.DataAccessLayer.Implementations
{
    public class AccessCompany : IAccessCompany
    {
        /// <summary>
        /// Возвращает объект компании по уникальному идентификатору
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Company FindById(int Id)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();

                var SqlCode = @"SELECT * FROM Company where Id=@id";

                var company = connection.QuerySingleOrDefault<Company>(SqlCode, new { Id });

                connection.Close();

                return company;
            }
        }

        /// <summary>
        /// Возвращает объект компании по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Company FindByName(string name)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();

                var SqlCode = @"SELECT * FROM Company where Name=@name";

                var company = connection.QuerySingleOrDefault<Company>(SqlCode, new { name });

                connection.Close();

                return company;
            }
        }
    }
}
