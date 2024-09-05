using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Domain;
using RentACar.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // user
                await _context.Users.AddAsync(employee.User);

                // employee
                await _context.Employers.AddAsync(employee);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employers.Include(e => e.User).FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return false;
            }

            _context.Users.Remove(employee.User);
            _context.Employers.Remove(employee);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employers.Include(e => e.User).ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employers.Include(e => e.User).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var employeeEntity = await _context.Employers.Include(e => e.User).FirstOrDefaultAsync(e => e.Id == employee.Id);

                if (employeeEntity == null)
                {
                    return false;
                }

                employeeEntity.User.Name = employee.User.Name;
                employeeEntity.User.Email = employee.User.Email;
                employeeEntity.PhoneNumber = employee.PhoneNumber;
                employeeEntity.Address = employee.Address;
                employeeEntity.Role = employee.Role;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
           
        }
    }
}
