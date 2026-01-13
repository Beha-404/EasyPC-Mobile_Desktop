using EasyPC.Model.Requests.OrderRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Model;

namespace EasyPC.Services.Interfaces;

public interface IOrderService
{
    public PagedResult<Order> Get(OrderSearchObjects searchObject);
    public Order? Insert(OrderInsertRequest insert);
    public Order? Update(int id, OrderDetailsUpdateRequest updateRequest);
    public bool Delete(int id);
    public Order? GetById(int id);
}
