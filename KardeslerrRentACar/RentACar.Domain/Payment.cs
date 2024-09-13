using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; } = new User();
    }

}
