using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccessLayer.Interfaces;
using WebService.Model;

namespace WebService.DataAccessLayer.Implementations
{
    public class AccessCompany : IAccessCompany
    {
        /// <summary>
        /// По имени ищет из базы данных компанию
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
