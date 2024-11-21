using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Application.Templates.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyManager.Application.Templates.Queries
{
    public record GetTemplatesDetailsQuery : IRequest<TemplatesDetailsDTO>
    {
        public Guid Id { get; set; }
    }
    public class GetTemplatesDetailsQueryHandler : IRequestHandler<GetTemplatesDetailsQuery, TemplatesDetailsDTO>
    {
        private readonly IApplicationDbContext _context;
        public GetTemplatesDetailsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TemplatesDetailsDTO> Handle(GetTemplatesDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var tamplatedetails = await _context.Templates
                .Where(t => t.Id == request.Id)
                .Select(t => new TemplatesDetailsDTO
                {
                    Id = t.Id,

                    Description = t.Description,
                    Section = t.Section
                    //Family=t.Family.Name
                }).FirstOrDefaultAsync(cancellationToken);
            if (tamplatedetails == null)
            {
                throw new Exception($"Template with Id{request.Id} was not faund");
            }
            return tamplatedetails;
        }
    }
}
