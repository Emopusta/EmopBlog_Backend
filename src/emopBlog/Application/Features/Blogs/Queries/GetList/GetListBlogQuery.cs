using Application.Features.Blogs.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Blogs.Constants.BlogsOperationClaims;

namespace Application.Features.Blogs.Queries.GetList;

public class GetListBlogQuery : IRequest<GetListResponse<GetListBlogListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListBlogs({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetBlogs";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBlogQueryHandler : IRequestHandler<GetListBlogQuery, GetListResponse<GetListBlogListItemDto>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public GetListBlogQueryHandler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBlogListItemDto>> Handle(GetListBlogQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Blog> blogs = await _blogRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListBlogListItemDto> response = _mapper.Map<GetListResponse<GetListBlogListItemDto>>(blogs);
            return response;
        }
    }
}