namespace XComm.Api.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();  //get all hanya untuk pemula, jangan dibikin di production
        T GetById(long id);
        T Create(T model);
    }
}
