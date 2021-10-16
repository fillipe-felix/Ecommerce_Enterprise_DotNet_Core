using System.Threading.Tasks;

namespace EE.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}