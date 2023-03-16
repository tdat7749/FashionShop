using FashionStore.ViewModel.Catalog.Order;
using FashionStore.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.Catalog.SOrder
{
    public interface IOrderService
    {
        //ADMIN
        Task<ApiResult<int>> CreateOrder(CreateOrderRequest request);
        Task<ApiResult<bool>> ChangeStatusOrder(ChangeStatusOrderRequest request);


        //CLIENT
        Task<ApiResult<bool>> CancelOrder(int id);
        Task<PagingResultApiBase<List<OrderVm>>> GetListOrdersById(PagingOrderRequest request);
    }
}
