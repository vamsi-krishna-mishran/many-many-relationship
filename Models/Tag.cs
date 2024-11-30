using System.ComponentModel.DataAnnotations;

namespace PostTags.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        public string? Temp {  get; set; }
        public IEnumerable<PostTag> PostTags { get; set; }
    }
}
