using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Common
{
    public class PagingSuccessResultApi<T> : PagingResultApiBase<T>
    {
        public T Data { get; set; }
        public double TotalPage { get; set; }
        public int TotalRecordAll { get; set; }
        public int TotalRecord { get; set; }
        public PagingSuccessResultApi(int totalRecordAll,double totalPage,int totalRecord,int pageSize,int pageIndex,T data)
        {
            Message = "Thành Công !";
            Success = true;
            TotalPage = totalPage;
            TotalRecord = totalRecord;
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data;
            TotalRecordAll = totalRecordAll;

        }
    }
}
