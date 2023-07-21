namespace eWallet.Repositories
{
    public interface IBaseRepository<T>
    {
        public DataContext Data { get; }
        public IEnumerable<T> GetAll();
        public IEnumerable<T> Where(Func<T, bool> p);
        public T? GetById(Guid id);
        public Task<Guid> Insert(T entity);
        public Task Update(T entity);
        public Task Delete(T entity);
    }
}
