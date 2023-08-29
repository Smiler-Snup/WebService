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
    public class AccessPassport : IAccessPassport
    {
        public Passport FindById(int id)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();

                var SqlCode = @"SELECT * FROM Passport where Id=@id";

                var passport = connection.QuerySingleOrDefault<Passport>(SqlCode, id);

                connection.Close();

                return passport;
            }
        }
    }
}
