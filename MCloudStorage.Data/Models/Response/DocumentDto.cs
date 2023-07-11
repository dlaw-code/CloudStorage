using MCloudStorage.Data.Entities.Enums;

namespace MCloudStorage.Data.Models.Response
{
    /// <summary>
    /// The document data transfer object.
    /// </summary>
    public class DocumentDto
    {
        /// <summary>
        /// Gets or sets the document file name.
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the document parent location.
        /// </summary>
        /// <remarks>This defaults to UserId</remarks>
        public string ParentLocation { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the id of user uploading the document.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the document file type.
        /// </summary>
        public FileType FileType { get; set; }
    }
}
