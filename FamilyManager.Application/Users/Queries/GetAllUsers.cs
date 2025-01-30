using FamilyManager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyManager.Application.Users.Queries
{
    public record GetAllUsersQuery : IRequest<List<UserDTO>>
    {

    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDTO>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllUsersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Country = u.Country,
                    Email = u.Email
                }).ToListAsync(cancellationToken);
        }
    }
}
