﻿using FamilyManager.Application.Common;
using MediatR;

namespace FamilyManager.Application.Familys.Commands
{
    public record DeleteFamilyCommand(Guid Id) : IRequest;

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteFamilyCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteFamilyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Families.FindAsync([request.Id], cancellationToken);

            _context.Families.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

