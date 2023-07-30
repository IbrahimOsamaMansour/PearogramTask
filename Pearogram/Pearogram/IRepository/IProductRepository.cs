using Pearogram.Models;
using System.Threading.Tasks;

namespace Pearogram.IRepository
{
    public interface IProductRepository
    {
        List<Product> GetAll();
       Task<Product> GetById(int id);
       Task< Product> GetByName(string name);
       Task< int> Insert(Product product);
        Task< int> Update( Product product);
        // change bill state to true

      Task<int>   Delete(int id);
    }
}
