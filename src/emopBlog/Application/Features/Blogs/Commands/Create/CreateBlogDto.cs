using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blogs.Commands.Create;
public class CreateBlogDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Text { get; set; }
}
