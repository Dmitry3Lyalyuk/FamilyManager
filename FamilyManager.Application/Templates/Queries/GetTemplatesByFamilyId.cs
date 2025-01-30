using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Application.Templates.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyManager.Application.Templates.Queries
{
    public record GetTemplatesByFamilyIdQuery : IRequest<List<TemplateDTO>>
    {
        public Guid FamilyId { get; set; }
        public GetTemplatesByFamilyIdQuery(Guid familyId)
        {
            FamilyId = familyId;
        }

        public class GetTemplatesByFamilyIdQueryHandler : IRequestHandler<GetTemplatesByFamilyIdQuery, List<TemplateDTO>>
        {
            private readonly IApplicationDbContext _context;

            public GetTemplatesByFamilyIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<TemplateDTO>> Handle(GetTemplatesByFamilyIdQuery request,
                CancellationToken cancellationToken)
            {
                return await _context.Templates
                    .Where(t => t.FamilyId == request.FamilyId)
                    .Select(t => new TemplateDTO()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Description = t.Description
                    }).ToListAsync(cancellationToken);
            }
        }
    }
}
