namespace OpenPOS_APP.Services.Interfaces;

public interface IModelService<T>
{
    public static abstract List<T> GetAll();

    public static abstract T FindByID(int id);

    public static abstract bool Delete(T obj);

    public static abstract bool Update(T obj);

    public static abstract T Create(T obj);
}