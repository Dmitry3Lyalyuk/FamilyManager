using FamilyManager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyManager.Application.Families.Queries
{
    public record GetAllFamiliesQuery : IRequest<List<FamilyDTO>>;

    public class GetAllFamiliesQueryHandler : IRequestHandler<GetAllFamiliesQuery, List<FamilyDTO>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllFamiliesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FamilyDTO>> Handle(GetAllFamiliesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Families
                .Select(f => new FamilyDTO
                {
                    Id = f.Id,
                    Name = f.Name,
                    Category = f.Category,
                    Brand = f.Brand
                }).ToListAsync(cancellationToken);
        }
    }
}
