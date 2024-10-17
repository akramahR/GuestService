using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GuestService.Data
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int GuestId { get; set; } // ID of the guest 
        public string RequestType { get; set; } // e.g., "Housekeeping", "Maintenance"
        public string Description { get; set; } // Detailed description of the request
        public string Status { get; set; } // e.g., "Pending", "Completed"
        public DateTime RequestDate { get; set; } // Date the request was made
    }


    public class UpdateRequestDto
    {
        public int GuestId { get; set; } // ID of the guest 
        public string RequestType { get; set; } // e.g., "Housekeeping", "Maintenance"
        public string Description { get; set; } // Detailed description of the request
        public string Status { get; set; } // e.g., "Pending", "Completed"
        public DateTime RequestDate { get; set; } // Date the request was made
    }
}
