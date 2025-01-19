namespace FamilyManager.Web.Requests
{
    public record FamilyUpdateRequest
    {
        public string Brand { get; set; }
        public string Name { get; set; }
    }
}
