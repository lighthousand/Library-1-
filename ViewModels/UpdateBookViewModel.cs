using Library.ViewModels.Interfaces;

namespace Library.ViewModels
{
    public class UpdateBookViewModel : IUpdateBookViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Authors { get; set; }
        public byte[]? ImageCover { get; set; }
    }
}
