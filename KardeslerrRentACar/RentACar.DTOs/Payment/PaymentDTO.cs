using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DTOs.Payment
{
    public class PaymentDTO
    {
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVC { get; set; }
        public double Amount { get; set; } // Ödenecek tutar
        public string TokenId { get; set; } // Stripe Token ID
        public int VehicleId { get; set; }
    }

}
