using ClinicManagementSystem.Application.Dtos.Feedback;
using ClinicManagementSystem.Domain.Abstractions;
using ClinicManagementSystem.Domain.Abstractions.IUnitOfWork;
using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Domain.Errors;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Feedback;

public class AddFeedbackCommandHandler
    : IRequestHandler<AddFeedbackCommand, Result<AddFeedbackResultDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddFeedbackCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AddFeedbackResultDto>> Handle(
        AddFeedbackCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Rating < 1 || request.Rating > 5)
            return Result.Failure<AddFeedbackResultDto>(FeedbackError.InvalidRating);

        var feedback = new Domain.Entities.Feedback
        {
            Id = Guid.NewGuid(),
            PatientName = request.PatientName,
            PatientPhone = request.PatientPhone,
            Comment = request.Comment,
            Rating = request.Rating,
            PatientId = request.PatientId,
            Date = DateTime.UtcNow
        };

        await _unitOfWork.FeedBackRepository.AddAsync(feedback);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new AddFeedbackResultDto
        {
            FeedbackId = feedback.Id,
            SubmittedAt = feedback.Date
        });
    }
}
