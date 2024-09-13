using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentACar.Application.Interfaces;
using RentACar.DTOs.Payment;

public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;
    private readonly StripeSettings _stripeSettings;

    public PaymentController(IPaymentService paymentService, IOptions<StripeSettings> stripeOptions)
    {
        _paymentService = paymentService;
        _stripeSettings = stripeOptions.Value;
    }
    public IActionResult Pay(int vehicleId, double price)
    {
        var model = new PaymentDTO
        {
            VehicleId = vehicleId,
            Amount = price
        };
        ViewData["StripePublishableKey"] = _stripeSettings.PublishableKey;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ProcessPayment(PaymentDTO model)
    {
        var charge = await _paymentService.ProcessPaymentAsync(model);

        if (charge.Status == "succeeded")
        {
            return RedirectToAction("Success");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Ödeme sırasında bir hata oluştu.");
            return View("Pay", model);
        }
    }

    public IActionResult Success()
    {
        return View();
    }
}
