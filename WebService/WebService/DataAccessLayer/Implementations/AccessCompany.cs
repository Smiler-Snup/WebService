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
        public Company FindByName(string name)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();

                var SqlCode = @"SELECT * FROM Company where Name=@name";

                var company = connection.QuerySingleOrDefault<Company>(SqlCode,name);

                connection.Close();

                return company;
            }
        }
    }
}
