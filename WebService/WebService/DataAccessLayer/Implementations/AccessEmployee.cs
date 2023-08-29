using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccessLayer.Interfaces;
using WebService.Model;
using Dapper;

namespace WebService.DataAccessLayer.Implementations
{
    public class AccessEmployee : IAccessEmployee
    {

        public int? AddEmployee(Employee employee,Passport passport)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();
                var SqlCodeInsert = @"INSERT INTO Employee (Name, Surname, Phone, CompanyId, DepartmentId) 
                                    OUTPUT INSERTED.Id
                                    VALUES('@Name', '@Surname', '@Phone', @CompanyId, @DepartmentId)";
                // Выполняем запрос на добавление и получение ID
                int? newEmployeeId = connection.QuerySingleOrDefault<int?>(SqlCodeInsert, employee);

                if(newEmployeeId!=null)
                {
                    var SqlCodeInsertPassport = @"INSERT INTO Passport (Id, Type, Number)
                                                  VALUES (@Id, '@Type', '@Number')";

                    passport.Id = (int)newEmployeeId;
                    connection.Execute(SqlCodeInsertPassport, passport);
                }


                connection.Close();
                return newEmployeeId;
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployeesByCompany(string companyName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployeesByDepartment(string departmentName)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(int employeeId, Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
