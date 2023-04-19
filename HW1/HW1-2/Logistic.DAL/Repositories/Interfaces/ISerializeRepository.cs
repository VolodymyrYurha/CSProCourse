namespace Logistic.DAL.Interfaces
{
    public interface ISerializeRepository<TEntity>
        where TEntity : class
    {
        string Create(List<TEntity> entities);

        List<TEntity> Read(string filename);
    }
}
