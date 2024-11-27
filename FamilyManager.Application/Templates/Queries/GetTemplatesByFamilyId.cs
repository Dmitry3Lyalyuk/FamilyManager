using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Application.Templates.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyManager.Application.Templates.Queries
{
    public record GetTemplatesByFamilyIdQueris : IRequest<List<TemplateDTO>>
    {
        public Guid FamilyId { get; set; }
        public GetTemplatesByFamilyIdQueris(Guid familyId)
        {
            FamilyId = familyId;
        }
        public class GetTemplatesByFamilyIdQueryHandler : IRequestHandler<GetTemplatesByFamilyIdQueris, List<TemplateDTO>>
        {
            private readonly IApplicationDbContext _context;

            public GetTemplatesByFamilyIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<TemplateDTO>> Handle(GetTemplatesByFamilyIdQueris request,
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
