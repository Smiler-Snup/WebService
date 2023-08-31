using Dapper;
using Microsoft.Data.SqlClient;
using WebService.DataAccessLayer.Interfaces;
using WebService.Model;

namespace WebService.DataAccessLayer.Implementations
{
    public class AccessPassport : IAccessPassport
    {
        /// <summary>
        /// Возвращает объект паспорта по уникальному идентификтору
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Passport FindById(int id)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();

                var SqlCode = @"SELECT * FROM Passport where Id=@id";

                var passport = connection.QuerySingleOrDefault<Passport>(SqlCode, new { id });

                connection.Close();

                return passport;
            }
        }
    }
}
