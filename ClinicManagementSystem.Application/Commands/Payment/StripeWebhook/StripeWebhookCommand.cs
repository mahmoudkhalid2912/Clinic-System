using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Payment.StripeWebhook;

public record StripeWebhookCommand(string JsonPayload, string StripeSignature) : IRequest<Result>;
