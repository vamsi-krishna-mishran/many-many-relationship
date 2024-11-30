using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PostTags.Context;
using PostTags.DTOs;
using PostTags.Models;

namespace PostTags.Repository
{
    public interface IPostRepository
    {
        public Task<PostDTO> AddPost(Post post);
        public Task<IEnumerable<PostTagsDTO>> GetAllPosts();
        public Task<PostTag> Associate(int postId, int tagId);
        public Task<PostTag> UnAssociate(int postId, int tagId);
        public Task<PostDTO> Delete(int postId);
    }
    public class PostRepository : IPostRepository
    {
        private readonly PostTagContext _context;
        private readonly IMapper _mapper;
        public PostRepository(PostTagContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PostDTO> AddPost(Post post)
        {
            var result=await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            var resultdto = _mapper.Map<PostDTO>(result.Entity);

            return resultdto;
        }

        public async Task<PostTag> Associate(int postId, int tagId)
        {
            var result=await _context.PostTags.AddAsync(new PostTag() { PostId= postId, TagId = tagId });   
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PostDTO?> Delete(int postId)
        {
            var result=await _context.Posts.FindAsync(postId);
            if(result is null)
            {
                return null;
            }
            _context.Posts.Remove(result);
            await _context.SaveChangesAsync();
            return _mapper.Map<PostDTO>(result);
        }

        public async Task<IEnumerable<PostTagsDTO>> GetAllPosts()
        {
            /*var result = await _context.Posts.ToListAsync();
            var resultdto=_mapper.Map<IEnumerable<PostDTO>>(result);
            IList<PostDTO> posts=new List<PostDTO>();
            foreach(var post in resultdto)
            {
                   var tags=await _context.PostTags.Where(pt=>pt.PostId==post.Id).Select(pt=>pt.TagId).ToListAsync();
                   var taglist=await _context.Tags.Where(tag=>tags.Contains(tag.Id)).ToListAsync();
                   var taglistdto=_mapper.Map<IEnumerable<TagDTO>>(taglist);
                   var postdto=_mapper.Map<PostDTO>(post);
                postdto.Tags = taglistdto;
                posts.Add(post);
            }
            return posts;*/
            List<PostTagsDTO> result=new List<PostTagsDTO>();
            var posts = await _context.Posts.ToListAsync();
            foreach(var post in posts)
            {
                int postId = post.Id;
                var result2 = _context.Tags
                        .Join(_context.PostTags,
                              t => t.Id,
                              pt => pt.TagId,
                              (t, pt) => new { Tag = t, PostTag = pt })
                        .Where(x => x.PostTag.PostId == postId)
                        .Select(x => new Tag
                        {
                            Id=x.Tag.Id,
                            Name=x.Tag.Name,
                            Temp=x.Tag.Temp
                        });


                Console.WriteLine(result2);

                result.Add(new PostTagsDTO()
                {
                    Post = _mapper.Map<PostDTO>(post),
                    Tags = _mapper.Map<List<TagDTO>>(result2)
                });

            }
            return result;


            /*var result = from tag in _context.Tags
                         join posttag in _context.PostTags
                         on tag.Id equals posttag.TagId
                         join post in _context.Posts
                         on posttag.PostId equals post.Id
                         group tag by new { post.Id, post.Title, post.Description } into postGroup
                         select new PostTagsDTO
                         {
                             Post = new PostDTO
                             {
                                 Id = postGroup.Key.Id,
                                 Title = postGroup.Key.Title,
                                 Description = postGroup.Key.Description
                             },
                             Tags = postGroup.Select(t => new TagDTO
                             {
                                 Id = t.Id,
                                 Name = t.Name,
                                 Temp = t.Temp
                             }).ToList()
                         };
            return result;*/


        }

        public async Task<PostTag> UnAssociate(int postId, int tagId)
        {
            var res = await _context.PostTags.Where(pt => pt.PostId == postId && pt.TagId == tagId).FirstOrDefaultAsync();
            if(res is null)
            {
                return res;
            }
            _context.PostTags.Remove(res);
            await _context.SaveChangesAsync();
            return res;

        }
    }
}
