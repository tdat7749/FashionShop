using FashionStore.Application.Services.System.CommentService;
using FashionStore.ViewModel.System.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("paging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagingComments([FromQuery]PagingCommentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _commentService.GetPagingComments(request);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Khách Hàng,Quản Trị Viên")]
        public async Task<IActionResult> CreateComment([FromBody]CreateCommentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _commentService.CreateComment(request);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Khách Hàng,Quản Trị Viên")]
        public async Task<IActionResult> UpdateComment([FromBody]UpdateCommentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _commentService.UpdateComment(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Khách Hàng,Quản Trị Viên")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _commentService.DeleteComment(id);
            return Ok(result);
        }
    }
}
