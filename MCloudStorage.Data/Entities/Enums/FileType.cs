namespace MCloudStorage.Data.Entities.Enums
{
    /// <summary>
    /// The types of files that can be uploaded to M-Cloud Storage.
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// Used for image files.
        /// </summary>
        Images = 0,

        /// <summary>
        /// Used for video files.
        /// </summary>
        Videos = 1,

        /// <summary>
        /// Used for document files.
        /// </summary>
        Documents = 2,
    }
}
