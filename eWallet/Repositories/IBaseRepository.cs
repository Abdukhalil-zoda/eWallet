namespace eWallet.Repositories
{
    public interface IBaseRepository<T>
    {
        public DataContext Data { get; }
        public IEnumerable<T> GetAll();
        public IEnumerable<T> Where(Func<T, bool> p);
        public T GetById(Guid id);
        public void Insert(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
