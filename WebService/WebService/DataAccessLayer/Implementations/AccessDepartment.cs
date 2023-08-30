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
    public class AccessDepartment : IAccessDepartment
    {
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
