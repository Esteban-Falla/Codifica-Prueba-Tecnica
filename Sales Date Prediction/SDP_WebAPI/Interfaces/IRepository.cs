namespace SDP_WebAPI.Interfaces;

public interface IRepository<T>
    where T : IElement, new()
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T> GetById(object id);
    public Task<int> Add(T element);
    public Task<T> Update(T element);
    public Task<int> Delete(T element);
}