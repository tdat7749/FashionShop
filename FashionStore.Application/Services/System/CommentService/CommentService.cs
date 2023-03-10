using FashionStore.Data.EF;
using FashionStore.Data.Entities;
using FashionStore.ViewModel.Common;
using FashionStore.ViewModel.System.Comment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.System.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly FashionStoreDbContext _context;
        public CommentService(FashionStoreDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateComment(CreateCommentRequest request)
        {
            var comment = new Comment()
            {
                CreatedAt = DateTime.Now,
                ProductId = request.ProductId,
                UserId = request.UserId,
                Value = request.Value
            };

            _context.Comments.Add(comment);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> DeleteComment(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null) return new ApiFailedResult<bool>($"Không tồn tại bình luận nào với Id = {commentId}");

            _context.Comments.Remove(comment);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<PagingResultApiBase<List<CommentVm>>> GetPagingComments(PagingCommentRequest request)
        {
            var query = from c in _context.Comments
                        join u in _context.Users on c.UserId equals u.Id
                        where c.ProductId == request.ProductId
                        orderby c.CreatedAt descending
                        select new { c, u };

            if (query.Count() > 0)
            {
                query = query.Skip(((request.PageIndex - 1) * request.PageSize)).Take(request.PageSize);
            }

            int totalRecordAll = await query.CountAsync();

            var listComments = await query.Select(x => new CommentVm()
            {
                Id = x.c.Id,
                UserId = x.u.Id,
                UserName = x.u.UserName,
                Value = x.c.Value,
                CreatedAt = x.c.CreatedAt.ToString()
            }).ToListAsync();

            int totalRecord = await query.CountAsync();
            double totalPage = Math.Ceiling((double)totalRecordAll / request.PageSize);

            return new PagingSuccessResultApi<List<CommentVm>>(totalRecordAll, totalPage, totalRecord, request.PageSize, request.PageIndex, listComments);
        }

        public async Task<ApiResult<bool>> UpdateComment(UpdateCommentRequest request)
        {
            var comment = await _context.Comments.FindAsync(request.CommentId);
            if (comment == null) return new ApiFailedResult<bool>($"Không tồn tại bình luận nào với Id = {request.CommentId}");

            comment.Value = request.NewValue;
            _context.Comments.Update(comment);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }
    }
}
