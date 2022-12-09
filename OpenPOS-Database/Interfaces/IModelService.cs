namespace OpenPOS_Database.Interfaces;

public interface IModelService<T>
{
    public abstract List<T> GetAll();

    public abstract T FindByID(int id);

    public abstract bool Delete(T obj);

    public abstract bool Update(T obj);

    public abstract T Create(T obj);
}