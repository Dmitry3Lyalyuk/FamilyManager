using FamilyManager.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyManager.Application.Families.Commands.Create
{
    public record CreateFamilyCommand : IRequest<Guid>
    {
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
    }

}
