﻿using FamilyManager.Domain.Enums;

namespace FamilyManager.Application.Templates.Queries.DTOs
{
    public class TemplatesDetailsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Section Section { get; set; }
        public string Familyname { get; set; }
    }
}
