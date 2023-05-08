namespace Library.ViewModels.Interfaces
{
    public interface IViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }        
        public byte[] ImageCover { get; set; }        
    }
}
