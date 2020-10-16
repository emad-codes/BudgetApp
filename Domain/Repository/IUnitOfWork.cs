using System.Threading.Tasks;

namespace Budget.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}