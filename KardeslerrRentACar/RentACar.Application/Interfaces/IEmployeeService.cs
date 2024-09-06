using RentACar.DTOs.Employer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<GetEmployeesDTO>> GetEmployeesAsync();

        Task<GetEmployeeDTO?> GetEmployeeAsync(int id);

        Task <GetEmployeeDTO?> UpdateEmployeeAsync(UpdateEmployeeDTO updateEmployee);

        Task<bool> DeleteEmployeeAsync(int id);
    }
}
