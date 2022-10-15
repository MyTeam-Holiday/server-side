namespace myteam.holiday.Domain.Services
{
    public interface IGenericAppDbService<T>
    {
        Task<T> Create(T entity);
        Task<T> GetValue(Guid id);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<T>> GetAllValues();
        Task<T> Update(Guid id, T entity);
    }
}
