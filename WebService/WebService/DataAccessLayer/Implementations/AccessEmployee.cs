using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using WebService.DataAccessLayer.Interfaces;
using WebService.Model;
using Dapper;
using WebService.DataAccessLayer.DTO;

namespace WebService.DataAccessLayer.Implementations
{
    public class AccessEmployee : IAccessEmployee
    {
        /// <summary>
        /// Добавляет сотрудника и его паспорт в систему
        /// Транзакция считается единой, в случае ошибки на одном из этапов транзакция откатывается
        /// </summary>
        /// <param name="employeeDTO"></param>
        /// <returns></returns>
        public int? AddEmployee(EmployeeDTO employeeDTO)
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

                        int? newEmployeeId = connection.QuerySingleOrDefault<int?>(SqlCodeInsertEmployee, 
                            new {
                                    Name=employeeDTO.Name,
                                    Surname=employeeDTO.Surname,
                                    Phone = employeeDTO.Phone,
                                    CompanyId =employeeDTO.CompanyId,
                                    DepartmentId = employeeDTO.DepartmentId
                                }, 
                            transaction);

                        if (newEmployeeId != null)
                        {
                            var SqlCodeInsertPassport = @"INSERT INTO Passport (Id, Type, Number)
                                                 VALUES (@Id, @Type, @Number)";

                            connection.Execute(SqlCodeInsertPassport, new 
                            {
                                Id = newEmployeeId,
                                Type=employeeDTO.Type,
                                Number = employeeDTO.Number
                            }
                            , transaction);
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


        /// <summary>
        /// Удаление сотрудника и его паспорта
        /// Успешной ситуацией считается, когда удалено две строки из базы данных
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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
                    var necessaryCountDeletedRows = 2;
                    if (result == necessaryCountDeletedRows)
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

        /// <summary>
        /// Возвращает коллекцию сотрудников определенной компании
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeesByCompany(Company company)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();

                var SqlCodeSelectEmployees = @"SELECT * FROM Employee WHERE CompanyId = @CompanyId";

                var result = connection.Query<Employee>(SqlCodeSelectEmployees, new { CompanyId = company.Id });

                return result;
            }

        }

        /// <summary>
        /// Возвращает коллекцию сотрудников определенного типа отдела
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeesByDepartment(Company company,Department department)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();

                var SqlCodeSelectEmployees = @"SELECT * FROM Employee WHERE CompanyId = @CompanyId and DepartmentId = @DepartmentId";

                var result = connection.Query<Employee>(SqlCodeSelectEmployees, new { CompanyId = company.Id, DepartmentId = department.Id });

                return result;
            }
        }

        /// <summary>
        /// Обновляет поля сотрудника
        /// Обновлению подлежат только те поля, что были явно заданы
        /// Обновление происходит в два этапа, которые включены в одну транзакции, в случае ошибки изменения в базе данных откатываются
        /// </summary>
        /// <param name="employeeDTO"></param>
        /// <returns></returns>
        public bool UpdateEmployee(EmployeeDTO employeeDTO)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {

                    try
                    {
                        //Обновляем если хотя бы одно поле задано для сотрудника
                        if (employeeDTO.Name != null || employeeDTO.Surname != null || employeeDTO.Phone != null ||
                            employeeDTO.CompanyId != null || employeeDTO.DepartmentId != null)
                        {

                            string SqlCodeUpdateEmployees = "UPDATE Employee SET ";
                            if (employeeDTO.Name != null) SqlCodeUpdateEmployees += "Name = @Name, ";
                            if (employeeDTO.Surname != null) SqlCodeUpdateEmployees += "Surname = @Surname, ";
                            if (employeeDTO.Phone != null) SqlCodeUpdateEmployees += "Phone = @Phone, ";
                            if (employeeDTO.CompanyId != null) SqlCodeUpdateEmployees += "CompanyId = @CompanyId, ";
                            if (employeeDTO.DepartmentId != null) SqlCodeUpdateEmployees += "DepartmentId = @DepartmentId, ";

                            if (SqlCodeUpdateEmployees.EndsWith(", "))
                                SqlCodeUpdateEmployees = SqlCodeUpdateEmployees.Remove(SqlCodeUpdateEmployees.Length - 2);

                            SqlCodeUpdateEmployees += " WHERE Id = @Id";

                            connection.Execute(SqlCodeUpdateEmployees, employeeDTO, transaction);
                        }

                        //Обновляем если хотя бы одно поле задано для паспорта
                        if (!string.IsNullOrEmpty(employeeDTO.Number) || !string.IsNullOrEmpty(employeeDTO.Type))
                        {
                            string SqlCodeUpdatePassport = "UPDATE Passport SET ";
                            if (employeeDTO.Type != null) SqlCodeUpdatePassport += "Type = @Type, ";
                            if (employeeDTO.Number != null) SqlCodeUpdatePassport += "Number = @Number, ";
                            SqlCodeUpdatePassport = SqlCodeUpdatePassport.TrimEnd(',', ' ');

                            SqlCodeUpdatePassport += " WHERE Id = @Id";

                            connection.Execute(SqlCodeUpdatePassport, employeeDTO, transaction);
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch(Exception e)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
