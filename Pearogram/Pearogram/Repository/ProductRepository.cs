using Microsoft.EntityFrameworkCore;
using Pearogram.IRepository;
using Pearogram.Models;
using System.Security.Cryptography;

namespace Pearogram.Repository
{
    public class ProductRepository : IProductRepository
    {

        PearogramContext _context;
       public ProductRepository(PearogramContext context)
        {
            _context = context; 
        }
        public async Task< int >Delete(int id)
        {
            Product product = await GetById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }

        public List<Product> GetAll()
        {
            List<Product> products = _context.Products.Include(s =>s.Supplier).ToList();
            return products;
        }

        public async Task<Product>GetById(int id)
        {
            Product product = await _context.Products.Include(s => s.Supplier).FirstOrDefaultAsync(v =>v.productId==id);
            return product;
        }

        public async Task< Product> GetByName(string name)
        {
            Product product = await _context.Products.Where(p => p.productName == name).FirstOrDefaultAsync();
            return product;
        }
        public async Task< int> Insert(Product product)
        {
            try
            {
                var r=_context.Products.AddAsync(product);
                _context.SaveChangesAsync();
                return 1;
            }
            catch { return 0; }

        }

        public async Task<int> Update(Product product)
        {
            Product OldProduct =await GetById(product.productId);
            if (OldProduct != null) 
            {
                
                OldProduct.productName = product.productName;
                OldProduct.QuentityPerUnit = product.QuentityPerUnit;
                OldProduct.ReorderLevel = product.ReorderLevel;
                OldProduct.SupplierID= product.SupplierID;  
                OldProduct.unitInStock = product.unitInStock;
                OldProduct.UnitsInOrder = product.UnitsInOrder;
                _context.SaveChangesAsync(); 
                return 1;
            }
            return 0;
        }
    }
}
