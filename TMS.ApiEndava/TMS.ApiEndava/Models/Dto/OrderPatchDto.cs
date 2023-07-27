namespace TMS.ApiEndava.Models.Dto
{
    public class OrderPatchDto
    {
        public long OrderId { get; set; }

        public double totalPrice { get; set; }

        public int NumberOfTickets { get; set; }

    }
}
