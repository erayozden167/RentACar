using RentACar.DTOs.Payment;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<Charge> ProcessPaymentAsync(PaymentDTO model);
    }
}
