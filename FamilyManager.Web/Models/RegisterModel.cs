﻿using FamilyManager.Domain.Enums;
using MediatR;

namespace FamilyManager.Web.Models
{
    public record RegisterModel : IRequest<Guid>
    {
        public Status Status { get; set; }
        public Country Country { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
