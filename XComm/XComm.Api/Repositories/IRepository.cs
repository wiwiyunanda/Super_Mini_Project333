using ViewModel;

namespace XComm.Api.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();  //get all hanya untuk pemula, jangan dibikin di production
        T GetById(long id);
        T Create(T model);

        ResponseResult Pagination(int pageNum, int rows, string search, string orderBy, Sorting sort);

        T Update (T model);
        T ChangeStatus(long id, bool status);
    }
}
