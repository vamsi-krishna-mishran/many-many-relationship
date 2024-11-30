using AutoMapper;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostTags.DTOs;
using PostTags.Models;
using PostTags.Repository;

namespace PostTags.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repo;
        private readonly IMapper _mapper;
        public PostController(IPostRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllPosts();
                return Ok(result);
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                string? inner_message = ex.InnerException?.Message;
                return StatusCode(500, new {message, inner_message });

            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostDTO post)
        {
            try
            {
                var postentity = _mapper.Map<Post>(post);
                var result = await _repo.AddPost(postentity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string? inner_message = ex.InnerException?.Message;
                return StatusCode(500, new { message, inner_message });

            }
        }

        ////api/projects/{projectId}/tasks/{taskId}`:
        [HttpPost("{projectId}/tasks/{taskId}")]
        public async Task<IActionResult> Associate([FromRoute] int projectId, [FromRoute] int taskId)
        {
            try
            {
                var result = await _repo.Associate(projectId, taskId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string? inner_message = ex.InnerException?.Message;
                return StatusCode(500, new { message, inner_message });

            }
        }
        [HttpDelete("{projectId}/tasks/{taskId}")]
        public async Task<IActionResult> AssociateDel([FromRoute] int projectId, [FromRoute] int taskId)
        {
            try
            {
                var result = await _repo.UnAssociate(projectId, taskId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string? inner_message = ex.InnerException?.Message;
                return StatusCode(500, new { message, inner_message });

            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            try
            {
                var result=await _repo.Delete(Id);
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
