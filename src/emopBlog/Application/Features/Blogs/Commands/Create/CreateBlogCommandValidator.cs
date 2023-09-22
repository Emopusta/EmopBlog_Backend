using FluentValidation;

namespace Application.Features.Blogs.Commands.Create;

public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
{
    public CreateBlogCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Text).NotEmpty();
        RuleFor(c => c.Image).NotEmpty();
        RuleFor(c => c.Author).NotEmpty();
    }
}