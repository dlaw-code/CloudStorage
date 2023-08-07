using System.ComponentModel.DataAnnotations.Schema;

namespace MCloudStorage.Data.Entities
{
    public class SharedFile
    {
        
        public int Id { get; set; }
        public int DocumentId { get; set; } // Foreign key to the Document entity
        public string SenderUserId { get; set; } // ID of the user who shared the file
        public string ReceiverUserId { get; set; } // ID of the user who received the shared file
        public DateTime SharedAt { get; set; } // Timestamp when the file was shared
        //public Document SharedDocument { get; set; }
        // Navigation property to the Document entity (one-to-one relationship)
        public virtual Document SharedDocument { get; set; }
    }
}
