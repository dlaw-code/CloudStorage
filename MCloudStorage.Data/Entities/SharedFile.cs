using System.ComponentModel.DataAnnotations.Schema;

namespace MCloudStorage.Data.Entities
{
    public class SharedFile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime SharedAt { get; set; }

        [ForeignKey("Document")]
        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
