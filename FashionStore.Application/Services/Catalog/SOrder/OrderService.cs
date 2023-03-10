using FashionStore.Application.Services.Catalog.SProduct;
using FashionStore.Data.EF;
using FashionStore.Data.Entities;
using FashionStore.Data.Enums;
using FashionStore.ViewModel.Catalog.Order;
using FashionStore.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.Catalog.SOrder
{
    public class OrderService : IOrderService
    {
        private readonly FashionStoreDbContext _context;
        private readonly IProductService _productService;
        public OrderService(FashionStoreDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<ApiResult<bool>> ChangeStatusOrder(ChangeStatusOrderRequest request)
        {
            var order = await _context.Orders.FindAsync(request.OrderId);
            if (order == null) return new ApiFailedResult<bool>($"Không tồn tại đơn hàng với Id = {request.OrderId}");

            if (request.StatusId == OrderStatus.Cancelled)
            {
                var listOrderDetail = from od in _context.OrderDetails
                                      where od.OrderId == request.OrderId
                                      select od;
                await _productService.PlusStock(listOrderDetail.ToList());
            }

            order.Status = request.StatusId;
            _context.Orders.Update(order);

            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<int>> CreateOrder(CreateOrderRequest request)
        {
            var listDetailOrder = new List<OrderDetail>();

            foreach (var item in request.Details)
            {
                OrderDetail detail = new OrderDetail();
                detail.SubTotal = item.SubTotal;
                detail.Price = item.Price;
                detail.Quantity = item.Quantity;
                detail.ProductId = item.ProductId;
                detail.Color = item.Color;
                detail.Size = item.Size;
                listDetailOrder.Add(detail);
            }

            var order = new Order()
            {
                UserId = request.UserId,
                Total = request.Total,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Status = OrderStatus.Processing,
                CreatedAt = DateTime.Now,
                FullName = request.FullName,
                Note = request.Note,
                OrderDetails = listDetailOrder
            };

            _context.Orders.Add(order);

            // sau khi đặt hàng xong sẽ - đi số lượng tồn của sản phẩm
            await _productService.MinusStock(listDetailOrder);
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }

        public async Task<PagingResultApiBase<List<OrderVm>>> GetListOrdersById(PagingOrderRequest request)
        {
            var query = from u in _context.Users
                        join o in _context.Orders on u.Id equals o.UserId
                        where u.Id == request.UserId
                        select new { u, o };

            var listOrderDetails = from od in _context.OrderDetails
                                   join p in _context.Product on od.ProductId equals p.Id
                                   select new { p, od };


            int totalRecordAll = await query.CountAsync();

            if (query.Count() > 0)
            {
                query = query.Skip(((request.PageIndex - 1) * request.PageSize)).Take(request.PageSize).OrderBy(x => x.o.CreatedAt);
            }

            var listOrders = await query.Select(x => new OrderVm()
            {
                Id = x.o.Id,
                Total = x.o.Total,
                Address = x.o.Address,
                PhoneNumber = x.o.PhoneNumber,
                Status = x.o.Status.ToString(),
                CreatedAt = x.o.CreatedAt.ToString(),
                FullName = x.o.FullName,
                Note = x.o.Note,
                OrderDetails = listOrderDetails.Where(a => a.od.OrderId == x.o.Id).Select(q => new DetailOrderVm()
                {
                    OrderId = x.o.Id,
                    ProductId = q.od.ProductId,
                    ProductName = q.p.Name,
                    Price = q.od.Price,
                    Quantity = q.od.Quantity,
                    SubTotal = q.od.SubTotal,
                    Thumbnail = q.p.Thumbnail,
                    Color = q.od.Color,
                    Size = q.od.Size
                }).ToList()
            }).ToListAsync();


            int totalRecord = await query.CountAsync();
            double totalPage = Math.Ceiling((double)totalRecordAll / request.PageSize);

            return new PagingSuccessResultApi<List<OrderVm>>(totalRecordAll,totalPage, totalRecord, request.PageSize, request.PageIndex, listOrders);
        }
    }
}
