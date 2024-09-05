using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain
{
    internal class Rentaller
    {
        public int Id { get; set; }

        public string GovIdNumber { get; set; }

        public string LicenseType { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public int RentDuration { get; set; }

        public int RentCount
        {
            get
            {
                if (Reservations.Any())
                {
                    return Reservations.Where(x => x.Status == "Onaylandı").Count();
                }
                return 0;
            }
        }

        // Relations
        public ICollection<Reservations> Reservations { get; set; }





    }
}
