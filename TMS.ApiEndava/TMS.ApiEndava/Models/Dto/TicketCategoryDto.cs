namespace TMS.ApiEndava.Models.Dto
{
    public class TicketCategoryDto
    {
        public long TicketCategoryId { get; set; }

        public string? DescriptionTicketCategory { get; set; }

        public double? Price { get; set; }

        public long? EventId { get; set; }

        public virtual Event? Event { get; set; }

    }
}
