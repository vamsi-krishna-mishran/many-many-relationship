using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostTags.Context;
using PostTags.DTOs;
using PostTags.Models;

namespace PostTags.Repository
{
    public interface ITagRepository
    {
        public Task<TagDTO> Add(Tag tag);
        public Task<IEnumerable<TagDTO>> GetAllTags();
    }
    public class TagRepository:ITagRepository
    {
        private readonly PostTagContext _context;
        private readonly IMapper _mapper;
        public TagRepository(PostTagContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TagDTO> Add(Tag tag)
        {
            var result = await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            var resultdto = _mapper.Map<TagDTO>(result.Entity);

            return resultdto;
        }

        public async Task<IEnumerable<TagDTO>> GetAllTags()
        {
            var result = await _context.Tags.ToListAsync();
            var resultdto = _mapper.Map<IEnumerable<TagDTO>>(result);
            return resultdto;
        }
    }
}
