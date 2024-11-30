namespace PostTags.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateTime PublishedAt { get; set; }

       // public IEnumerable<TagDTO>? Tags { get; set; }

    }
}
