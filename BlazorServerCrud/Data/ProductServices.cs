using BlazorServerCrud.Data.AppDbContext;
using BlazorServerCrud.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BlazorServerCrud.Data
{
    public class ProductServices
    {
        private readonly IDbContextFactory<AppDbContext.AppDbContext> _dbContextFactory;
        public ProductServices(IDbContextFactory<AppDbContext.AppDbContext> dbContextFactory) { 
          _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                using var context = await _dbContextFactory.CreateDbContextAsync();
                return await context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Product?>GetProductById(int id)
        {
            using var context= await _dbContextFactory.CreateDbContextAsync();
            return await context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Product?>UpdateProductAsync(Product updateProduct)
        {
            try
            {
                using var context= await _dbContextFactory.CreateDbContextAsync();
                var existProduct = await context.Products.FirstOrDefaultAsync(p =>p.Id == updateProduct.Id);
                if(existProduct is not null)
                {
                    existProduct.Name=updateProduct.Name;
                    existProduct.Price =updateProduct.Price;
                    await context.SaveChangesAsync();
                    return existProduct;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteProductAsync(int id)
        {
            try
            {
                using var context = await _dbContextFactory.CreateDbContextAsync();
                var existProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (existProduct is not null)
                {
                    context.Remove(existProduct);
                    await context.SaveChangesAsync();
                    
                }
            }
            catch(Exception ex) 
            { 
             throw new Exception(ex.Message);
            }
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {
                using var context = await _dbContextFactory.CreateDbContextAsync();
                if(product is not null)
                {
                    context.Products.Add(product);
                    await context.SaveChangesAsync();
                    return product;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
