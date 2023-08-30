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

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var SqlCodeInsertEmployee = @"INSERT INTO Employee (Name, Surname, Phone, CompanyId, DepartmentId) 
                                             OUTPUT INSERTED.Id
                                             VALUES(@Name, @Surname, @Phone, @CompanyId, @DepartmentId)";

                        int? newEmployeeId = connection.QuerySingleOrDefault<int?>(SqlCodeInsertEmployee, employee, transaction);

                        if (newEmployeeId != null)
                        {
                            var SqlCodeInsertPassport = @"INSERT INTO Passport (Id, Type, Number)
                                                 VALUES (@Id, @Type, @Number)";

                            passport.Id = (int)newEmployeeId;
                            connection.Execute(SqlCodeInsertPassport, passport, transaction);
                        }

                        transaction.Commit();
                        return newEmployeeId;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
            }
        }

        public bool DeleteEmployee(int Id)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var SqlCodeDeleteEmployee = @"DELETE FROM Passport
                                                WHERE Id = @Id;
                                             DELETE FROM Employee
                                                WHERE Id = @Id;";

                    var result = connection.Execute(SqlCodeDeleteEmployee, new { Id }, transaction);
                    if (result == 2)
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public IEnumerable<Employee> GetEmployeesByCompany(string companyName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployeesByDepartment(string departmentName)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEmployee(int employeeId, Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
