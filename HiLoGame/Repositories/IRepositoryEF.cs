namespace HiLoGame.Repositories
{
    public interface IRepositoryEF<I, T>
    {
        T FindByid(I id);
        IEnumerable<T> FindAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
