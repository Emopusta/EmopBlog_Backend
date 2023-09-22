using FluentValidation;

namespace Application.Features.Blogs.Commands.Update;

public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
{
    public UpdateBlogCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Text).NotEmpty();
        RuleFor(c => c.Image).NotEmpty();
        RuleFor(c => c.Author).NotEmpty();
    }
}