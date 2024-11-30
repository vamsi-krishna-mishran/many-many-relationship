namespace PostTags.DTOs
{
    public class PostTagsDTO
    {
        public PostDTO Post { get; set; }
        public IList<TagDTO> Tags { get; set; }
    }
}
