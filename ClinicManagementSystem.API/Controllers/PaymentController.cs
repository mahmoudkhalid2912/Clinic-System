using ClinicManagementSystem.Application.Commands.Payment.CreatePaymentIntent;
using ClinicManagementSystem.Application.Commands.Payment.StripeWebhook;
using ClinicManagementSystem.Application.Dtos.Payment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace ClinicManagementSystem.API.Controllers;

[Route("[controller]")]
[ApiController]
public class PaymentController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a Stripe PaymentIntent for a booking.
    /// Call this before showing the Stripe payment form to the user.
    /// Returns a client_secret that the frontend uses with Stripe.js to confirm payment.
    /// </summary>
    [HttpPost("create-intent/{bookingId:guid}")]
    [Authorize]
    public async Task<IActionResult> CreatePaymentIntent(
        Guid bookingId,
        [FromQuery] string currency = "usd",
        CancellationToken cancellationToken = default)
    {
        var command = new CreatePaymentIntentCommand(bookingId, currency);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToApiResponse("Payment intent created successfully");
    }

    /// <summary>
    /// Stripe webhook endpoint.
    /// Configure this URL in your Stripe Dashboard -> Webhooks.
    /// Listen for: payment_intent.succeeded, payment_intent.payment_failed
    /// </summary>
    [HttpPost("webhook")]
    [AllowAnonymous]
    public async Task<IActionResult> StripeWebhook(CancellationToken cancellationToken)
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync(cancellationToken);
        var stripeSignature = Request.Headers["Stripe-Signature"].ToString();

        var command = new StripeWebhookCommand(json, stripeSignature);
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsFailuer)
            return BadRequest(result.Error.Description);

        return Ok();
    }



    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmPaymentRequest request)
    {
        try
        {
            var service = new PaymentIntentService();

            var paymentIntent = await service.ConfirmAsync(request.PaymentIntentId);

            return Ok(new
            {
                success = true,
                status = paymentIntent.Status,
                id = paymentIntent.Id
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                message = ex.Message
            });
        }
    }
}
