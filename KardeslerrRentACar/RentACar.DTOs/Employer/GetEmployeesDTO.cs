using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DTOs.Employer
{
    public class GetEmployeesDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Role {  get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;       
              


    }
}
