using FashionStore.ViewModel.Common;
using FashionStore.ViewModel.System.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.System.CommentService
{
    public interface ICommentService
    {

        //Client
        Task<PagingResultApiBase<List<CommentVm>>> GetPagingComments(PagingCommentRequest request);
        Task<ApiResult<bool>> CreateComment(CreateCommentRequest request);
        Task<ApiResult<bool>> UpdateComment(UpdateCommentRequest request);
        Task<ApiResult<bool>> DeleteComment(int commentId);
    }
}
