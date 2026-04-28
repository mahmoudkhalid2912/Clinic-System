using ClinicManagementSystem.Application.Dtos.Payment;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Payment.CreatePaymentIntent;

public record CreatePaymentIntentCommand(Guid BookingId, string Currency = "usd") : IRequest<Result<PaymentIntentResponseDto>>;
