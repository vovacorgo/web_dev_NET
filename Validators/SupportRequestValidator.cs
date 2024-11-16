using FluentValidation;
using SupportCenter.Models;

namespace SupportCenter.Validators
{
    public class SupportRequestValidator : AbstractValidator<SupportRequest>
    {
        public SupportRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Subject is required.");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Message is required.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required.");
            RuleFor(x => x.RequestDate).NotNull().WithMessage("Request date is required.");
            RuleFor(x => x.Priority).NotEmpty().WithMessage("Priority is required.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required.");
            RuleFor(x => x.AssignedTo).NotEmpty().WithMessage("AssignedTo is required.");
            RuleFor(x => x.Resolution).NotEmpty().WithMessage("Resolution is required.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");

        }
    }
}
