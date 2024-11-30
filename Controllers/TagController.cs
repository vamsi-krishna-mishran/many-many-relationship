using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostTags.DTOs;
using PostTags.Models;
using PostTags.Repository;

namespace PostTags.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _repo;
        private readonly IMapper _mapper;
        public TagController(ITagRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllTags();
                return Ok(result);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string? inner_message = ex.InnerException?.Message;
                return StatusCode(500, new { message, inner_message });

            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TagDTO post)
        {
            try
            {
                var postentity = _mapper.Map<Tag>(post);
                var result = await _repo.Add(postentity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string? inner_message = ex.InnerException?.Message;
                return StatusCode(500, new { message, inner_message });

            }
        }
    }
}
