namespace Library.Models.Interfaces
{
    public interface IModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string[] Authors { get; set; }
        public byte[]? ImageCover { get; set; }
    }
}
