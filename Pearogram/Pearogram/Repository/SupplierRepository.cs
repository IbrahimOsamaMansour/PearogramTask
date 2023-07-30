using Microsoft.EntityFrameworkCore;
using Pearogram.IRepository;
using Pearogram.Models;

namespace Pearogram.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        PearogramContext _context;
        public SupplierRepository(PearogramContext context)
        {
            _context = context; 
        } 
        public async Task<bool> Delete(int id)
        {
            Supplier supplier =await GetById(id);
            if (supplier != null)
            {
                _context.Remove(supplier);
                _context.SaveChanges();
                return true; 
            }  
            return false;
        }

        public List<Supplier> GetAll()
        {
            List<Supplier> suppliers = _context.Suppliers.ToList(); 
               
            return suppliers;    
        }

        public async Task<Supplier> GetById(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task<bool> Insert(Supplier supplier)
        {
            try
            {
                _context.Suppliers.AddAsync(supplier);
               await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
           
        }

        public async Task<bool>     Update( Supplier supplier)
        {
            Supplier OldSupplier =await GetById(supplier.SupplierID);
            if (OldSupplier != null)
            {
                OldSupplier.SupplierName= supplier.SupplierName;
               await _context.SaveChangesAsync();
                return true;    
            }
            return false;
        }


        public async Task<Supplier> GetByName(string name)
        {
            Supplier supplier =await _context.Suppliers.Where(p => p.SupplierName == name).FirstOrDefaultAsync();
            return supplier;
        }
    }
}
