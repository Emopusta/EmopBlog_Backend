using Application.Features.Blogs.Constants;
using Application.Features.Blogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Blogs.Constants.BlogsOperationClaims;
using Microsoft.AspNetCore.Http;
using Application.Services.ImageService;

namespace Application.Features.Blogs.Commands.Create;

public class CreateBlogCommand : IRequest<CreatedBlogResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public CreateBlogDto CreateBlogDto { get; set; }
    public IFormFile Image { get; set; }

    public string[] Roles => new[] { Admin, Write, BlogsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetBlogs";

    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, CreatedBlogResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        private readonly BlogBusinessRules _blogBusinessRules;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ImageServiceBase _imageServiceBase;

        public CreateBlogCommandHandler(IMapper mapper, IBlogRepository blogRepository, BlogBusinessRules blogBusinessRules, IHttpContextAccessor httpContextAccessor, ImageServiceBase imageServiceBase)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
            _blogBusinessRules = blogBusinessRules;
            _httpContextAccessor = httpContextAccessor;
            _imageServiceBase = imageServiceBase;
        }

        public async Task<CreatedBlogResponse> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            Blog blog = _mapper.Map<Blog>(request.CreateBlogDto);
            blog.Author = _httpContextAccessor.HttpContext.User.Identity.Name ?? "(unauthorized)";
            blog.Image = await _imageServiceBase.UploadAsync(request.Image) ?? "empty";

            await _blogRepository.AddAsync(blog);

            CreatedBlogResponse response = _mapper.Map<CreatedBlogResponse>(blog);
            return response;
        }
    }
}