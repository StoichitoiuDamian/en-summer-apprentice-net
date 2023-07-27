namespace TMS.ApiEndava.Models.Dto
{
    public class OrderDto
    {
        public long OrderId { get; set; }

        public int NumberOfTickets { get; set; }

        public double TotalPrice { get; set; }

        public long ticketCategoryId { get; set; }

        public long UserId { get; set; }

    }
}
