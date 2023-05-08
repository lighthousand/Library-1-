using Library.Models.Interfaces;

namespace Library.Models
{
    public class Book: IBook
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string[] Authors { get; set; }
        public byte[]? ImageCover { get; set; }
    }
}