namespace GuestService.Data
{
    public class RequestDTO
    {
        public int GuestId { get; set; } // ID of the guest

        public string RequestType { get; set; } // e.g., "Housekeeping", "Maintenance"

        public string Description { get; set; }
    }
}
