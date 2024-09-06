using Microsoft.AspNetCore.Identity;
using RentACar.Application.Interfaces;
using RentACar.Domain;
using RentACar.DTOs.Employer;
using RentACar.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public EmployeeService(IEmployeeRepository employeeRepository, IPasswordHasher<User> passwordHasher)
        {
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            return await _employeeRepository.DeleteEmployeeAsync(id);
        }

        public async Task<GetEmployeeDTO?> GetEmployeeAsync(int id)
        {
            Employee? employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return null;
            }

            return ConvertToGetEmployeeDTO(employee);
        }

        public async Task<List<GetEmployeesDTO>> GetEmployeesAsync()
        {
            List<Employee> employees = await _employeeRepository.GetAllEmployeesAsync();
            if (employees == null)
            {
                return new List<GetEmployeesDTO>();
            }

            return employees.Select(e => new GetEmployeesDTO
            {
                Id = e.Id,
                Name = e.User.Name,
                Role = e.User.Role,
                PhoneNumber = e.PhoneNumber
            }).ToList();
        }

        public async Task<GetEmployeeDTO?> UpdateEmployeeAsync(UpdateEmployeeDTO updateEmployee)
        {
            Employee? employee = await _employeeRepository.GetEmployeeByIdAsync(updateEmployee.Id);
            if (employee == null || _passwordHasher.VerifyHashedPassword(employee.User, employee.User.PasswordHashed, updateEmployee.Password) != PasswordVerificationResult.Success) 
            {
                return null;
            }

            employee.User.Name = updateEmployee.Name;
            employee.Role = updateEmployee.Role;
            employee.PhoneNumber = updateEmployee.PhoneNumber;
            employee.Address = updateEmployee.Address;
            
            bool isUpdated = await _employeeRepository.UpdateEmployeeAsync(employee);
            if (isUpdated)
            {
                return null;
            }

            return ConvertToGetEmployeeDTO(employee);

        }

        private GetEmployeeDTO ConvertToGetEmployeeDTO(Employee employee)
        {
            return new GetEmployeeDTO
            {
                Id = employee.Id,
                GovIdNumber = employee.GovIdNumber,
                Name = employee.User.Name,
                LicenseType = employee.LicenseType,
                DateOfBirth = employee.DateOfBirth,
                PhoneNumber = employee.PhoneNumber,
                Role = employee.Role,
                Gender = employee.Gender,
                Address = employee.Address,
            };
        }
    }
}
