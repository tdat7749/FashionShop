using FashionStore.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.System.Comment
{
    public class PagingCommentRequest : PagingBase
    {
        public int ProductId { get; set; }
    }
}
