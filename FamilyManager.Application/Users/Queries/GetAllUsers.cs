using FamilyManager.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyManager.Application.Users.Querries
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
                    Email = u.Email
                }).ToListAsync(cancellationToken);

        }
    }
}
