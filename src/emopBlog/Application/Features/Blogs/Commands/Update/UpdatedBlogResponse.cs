using Core.Application.Responses;

namespace Application.Features.Blogs.Commands.Update;

public class UpdatedBlogResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Text { get; set; }
    public string Image { get; set; }
    public string Author { get; set; }
}