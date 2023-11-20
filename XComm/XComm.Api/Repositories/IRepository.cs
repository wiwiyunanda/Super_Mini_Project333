using ViewModel;

namespace XComm.Api.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();  //get all hanya untuk pemula, jangan dibikin di production
        T GetById(long id);
        T Create(T model);

        ResponseResult Pagination(int pageNum, int rows, string search, string orderBy, Sorting sort);

        ResponseResult Update (T model);
        ResponseResult ChangeStatus(long id, bool status);
    }
}
