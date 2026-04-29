using ClinicManagementSystem.API.Extensions;
using ClinicManagementSystem.Application.Commands.Feedback;
using ClinicManagementSystem.Domain.Abstractions.IUnitOfWork;
using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Infrastructure.persistence.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public FeedBackController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

       
        [HttpPost("add-feedback")]
        [AllowAnonymous]
        public async Task<IActionResult> AddFeedback([FromBody] AddFeedbackCommand command)
        {
            
            if (User.Identity?.IsAuthenticated == true)
                command.PatientId = User.GetUserId();

            var result = await _mediator.Send(command);

            return result.ToApiResponse("Feedback submitted successfully. Thank you!");
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var obj = await _unitOfWork.FeedBackRepository.GetAsync(x => x.Id == id);
            return Ok(obj);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var feedbacklist = await _unitOfWork.FeedBackRepository.GetAllAsync();
            return Ok(feedbacklist);
        }

        [HttpPut("Update{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Feedback entity)
        {
            if (entity == null)
                return BadRequest("feed back can not be null");

            var obj = await _unitOfWork.FeedBackRepository.GetAsync(x => x.Id == id);

            if (obj == null)
                return NotFound("FeedBack Not Found");

            _unitOfWork.FeedBackRepository.Update(obj);
            await _unitOfWork.SaveChangesAsync();
            return Ok(obj);
        }

        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var feedback = await _unitOfWork.FeedBackRepository.GetAsync(x => x.Id == id);

            if (feedback == null)
                return NotFound("Feedback not found");

            _unitOfWork.FeedBackRepository.Delete(feedback);
            await _unitOfWork.SaveChangesAsync();

            return Ok("Deleted successfully");
        }

        [HttpDelete("DeleteRange")]
        public async Task<IActionResult> DeleteRange([FromBody] List<Guid> ids)
        {
            var allFeedbacks = await _unitOfWork.FeedBackRepository.GetAllAsync();

            var feedbacks = allFeedbacks
                .Where(x => ids.Contains(x.Id))
                .ToList();

            if (!feedbacks.Any())
                return NotFound("No feedbacks found");

            _unitOfWork.FeedBackRepository.DeleteRange(feedbacks);
            await _unitOfWork.SaveChangesAsync();

            return Ok("Deleted successfully");
        }
    }
}

