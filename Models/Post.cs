using System.ComponentModel.DataAnnotations;

namespace PostTags.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string Author { get; set; } = null!;
        [Required]
        public DateTime PublishedAt { get; set; }

        public IEnumerable<PostTag> PostTags { get; set; }

    }
}
