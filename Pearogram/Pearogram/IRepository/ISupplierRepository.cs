using Pearogram.Models;

namespace Pearogram.IRepository
{
    public interface ISupplierRepository
    {
        List<Supplier> GetAll();
        Task< Supplier> GetById(int id);

        Task<Supplier> GetByName(string name);

        Task<bool> Insert(Supplier supplier);
         Task<bool> Update( Supplier supplier);
        // change bill state to true

        Task<bool> Delete(int id);
    }
}
