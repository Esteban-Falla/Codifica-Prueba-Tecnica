using SDP_WebAPI.Models;

namespace SDP_WebAPI.Interfaces;

public interface IOrderRepository:IRepository<OrderModel>
{
    public Task<IEnumerable<OrderModel>> GetByClientId(int clientId);
    public Task<int> Add(AddOrderModel element);
}