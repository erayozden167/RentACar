using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RentACar.Application.Interfaces;
using RentACar.Domain;
using RentACar.DTOs.Payment;
using RentACar.Infrastructure.Interfaces;
using Stripe;
using System.Security.Claims;

public class PaymentService : IPaymentService
{
    private readonly IOptions<StripeSettings> _stripeSettings;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IRenterRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PaymentService(
        IOptions<StripeSettings> stripeSettings,
        IPaymentRepository paymentRepository,
        IRenterRepository userRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _stripeSettings = stripeSettings;
        _paymentRepository = paymentRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Charge> ProcessPaymentAsync(PaymentDTO model)
    {
        var email = _httpContextAccessor.HttpContext.Request.Cookies["UserEmail"];

        if (string.IsNullOrEmpty(email))
        {
            throw new Exception("Kullanıcı email bilgisi bulunamadı.");
        }

        // Kullanıcıyı repository'den bul
        var renter = await _userRepository.GetRenterByMailAsync(email);

        if (renter == null)
        {
            throw new Exception("Kullanıcı bulunamadı.");
        }
        var options = new ChargeCreateOptions
        {
            Amount = (long)(model.Amount * 100), 
            Currency = "usd",
            Source = model.TokenId, 
            Description = "Rezervasyon Ödemesi"
        };

        var service = new ChargeService();
        Charge charge = await service.CreateAsync(options);

        
        if (charge.Status == "succeeded")
        {
            var payment = new Payment
            {
                Amount = model.Amount,
                TransactionId = charge.Id,
                PaymentDate = DateTime.Now,
                UserId = renter.UserId,
                User = renter.User
            };

            await _paymentRepository.SavePaymentAsync(payment);
            renter.User.Payment.Add(payment);
            await _userRepository.UpdateUserAsync(renter.User);

        }

        return charge;
    }
}
